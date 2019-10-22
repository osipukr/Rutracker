using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IDialogRepository _dialogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IMessageRepository messageRepository, IDialogRepository dialogRepository, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _dialogRepository = dialogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Message>> ListAsync(int dialogId, string userId)
        {
            Guard.Against.LessOne(dialogId, "Invalid dialog id.");
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");

            if (!await _dialogRepository.ExistAsync(x => x.UserDialogs.Any(ud => ud.DialogId == dialogId && ud.UserId == userId)))
            {
                throw new RutrackerException($"The dialog with id '{dialogId}' not found.", ExceptionEventTypes.InvalidParameters);
            }

            var messages = await _messageRepository.GetAll(x => x.DialogId == dialogId).ToListAsync();

            Guard.Against.NullNotFound(messages, $"The messages with dialog id '{dialogId}' not found.");

            return messages;
        }

        public async Task<Message> FindAsync(int id, string userId)
        {
            Guard.Against.LessOne(id, "Invalid message id.");
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");

            var message = await _messageRepository.GetAsync(x => x.Id == id && x.UserId == userId);

            Guard.Against.NullNotFound(message, $"The message with id '{id}' not found.");

            return message;
        }

        public async Task<Message> AddAsync(Message message)
        {
            Guard.Against.NullNotValid(message, "Invalid message.");
            Guard.Against.NullOrWhiteSpace(message.Text, message: "Invalid message text.");
            Guard.Against.NullOrWhiteSpace(message.UserId, message: "Invalid message user id.");
            Guard.Against.LessOne(message.DialogId, "Invalid message dialog id.");

            if (!await _dialogRepository.ExistAsync(message.DialogId))
            {
                throw new RutrackerException($"The dialog with id '{message.DialogId}' not found.", ExceptionEventTypes.InvalidParameters);
            }

            var result = _messageRepository.Create();

            result.Text = message.Text;
            result.UserId = message.UserId;
            result.DialogId = message.DialogId;
            result.SentAt = DateTime.UtcNow;

            await _messageRepository.AddAsync(result);
            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<Message> DeleteAsync(int id, string userId)
        {
            var message = await FindAsync(id, userId);

            _messageRepository.Remove(message);

            await _unitOfWork.CompleteAsync();

            return message;
        }
    }
}