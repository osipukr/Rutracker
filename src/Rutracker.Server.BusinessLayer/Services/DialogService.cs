using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class DialogService : IDialogService
    {
        private readonly IDialogRepository _dialogRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public DialogService(IDialogRepository dialogRepository, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _dialogRepository = dialogRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Dialog>> ListAsync(string userId)
        {
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");

            var dialogs = await _dialogRepository.GetAll(x => x.UserDialogs.Any(y => y.UserId == userId)).ToListAsync();

            Guard.Against.NullNotFound(dialogs, "The dialogs not found.");

            return dialogs;
        }

        public async Task<Dialog> FindAsync(int id, string userId)
        {
            Guard.Against.LessOne(id, "Invalid dialog id.");
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");

            var dialog = await _dialogRepository.GetAsync(x => x.UserDialogs.Any(y => y.DialogId == id && y.UserId == userId));

            Guard.Against.NullNotFound(dialog, $"The dialog with id '{id}' not found.");

            return dialog;
        }

        public async Task<Dialog> FindByOwnerAsync(int id, string userId)
        {
            Guard.Against.LessOne(id, "Invalid dialog id.");
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");

            var dialog = await _dialogRepository.GetAsync(x => x.Id == id && x.UserId == userId);

            Guard.Against.NullNotFound(dialog, $"The dialog with id '{id}' not found.");

            return dialog;
        }

        public async Task<Dialog> AddAsync(Dialog dialog, IEnumerable<string> userIds)
        {
            Guard.Against.NullNotValid(dialog, "Invalid dialog.");
            Guard.Against.NullOrWhiteSpace(dialog.Title, message: "Invalid dialog title.");
            Guard.Against.NullOrWhiteSpace(dialog.UserId, message: "Invalid dialog user id.");
            Guard.Against.NullNotValid(userIds, "Invalid user ids.");

            var ids = userIds.Where(x => !string.IsNullOrWhiteSpace(x)).Concat(new[] { dialog.UserId }).Distinct().ToArray();

            if (ids.Length == 1)
            {
                throw new RutrackerException("No selected user.", ExceptionEventTypes.InvalidParameters);
            }

            var users = new List<User>(ids.Length);

            foreach (var id in ids)
            {
                var user = await _userManager.FindByIdAsync(id);

                Guard.Against.NullNotValid(user, $"The user with id '{id}' not found.");

                users.Add(user);
            }

            var result = _dialogRepository.Create();

            result.Title = dialog.Title;
            result.UserId = dialog.UserId;
            result.UserDialogs = users.Select(user => new UserDialog
            {
                Dialog = result,
                User = user
            }).ToArray();

            await _dialogRepository.AddAsync(result);
            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<Dialog> DeleteAsync(int id, string userId)
        {
            var dialog = await FindByOwnerAsync(id, userId);

            _dialogRepository.Remove(dialog);

            await _unitOfWork.CompleteAsync();

            return dialog;
        }
    }
}