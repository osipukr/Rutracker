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

            var dialogs = await _dialogRepository.GetAll(x => x.UserDialogs.Any(ud => ud.UserId == userId)).ToListAsync();

            Guard.Against.NullNotFound(dialogs, "The dialogs not found.");

            return dialogs;
        }

        public async Task<Dialog> FindAsync(int id, string userId)
        {
            Guard.Against.LessOne(id, "Invalid dialog id.");
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");

            var dialog = await _dialogRepository.GetAsync(x => x.Id == id && x.UserDialogs.Any(ud => ud.UserId == userId));

            Guard.Against.NullNotFound(dialog, "The dialog not found.");

            return dialog;
        }

        public async Task<Dialog> AddAsync(Dialog dialog, IEnumerable<string> userIds)
        {
            Guard.Against.NullNotValid(dialog, "Invalid dialog.");
            Guard.Against.NullOrWhiteSpace(dialog.Title, message: "Invalid dialog title.");
            Guard.Against.NullNotValid(userIds, "Invalid user ids.");

            var users = await Task.WhenAll(userIds.Select(_userManager.FindByIdAsync));

            if (users.Length == 0)
            {
                throw new RutrackerException("No selected user.", ExceptionEventTypes.NotValidParameters);
            }

            var result = _dialogRepository.Create();

            result.Title = dialog.Title;
            result.UserDialogs = users.Select(user => new UserDialog
            {
                Dialog = result,
                User = user
            }).ToList();

            await _dialogRepository.AddAsync(result);
            await _unitOfWork.CompleteAsync();

            return result;
        }
    }
}