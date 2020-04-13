using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;
using Rutracker.Server.BusinessLayer.Services.Base;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class UserService : Service, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IStorageService _storageService;
        private readonly IDateService _dateService;

        public UserService(UserManager<User> userManager, IStorageService storageService, IDateService dateService) : base(null)
        {
            _userManager = userManager;
            _storageService = storageService;
            _dateService = dateService;
        }

        public async Task<IPagedList<User>> ListAsync(IUserFilter filter)
        {
            Guard.Against.OutOfRange(filter.Page, Constants.Filter.PageRangeFrom, Constants.Filter.PageRangeTo, Resources.Page_InvalidPageNumber);
            Guard.Against.OutOfRange(filter.PageSize, Constants.Filter.PageSizeRangeFrom, Constants.Filter.PageSizeRangeTo, Resources.PageSize_InvalidPageSizeNumber);

            var query = _userManager.Users.OrderBy(x => x.AddedDate).AsNoTracking();

            var pagedList = await ApplyFilterAsync(query, filter);

            Guard.Against.NullNotFound(pagedList.Items, Resources.User_NotFoundList_ErrorMessage);

            return pagedList;
        }

        public async Task<User> FindAsync(string id)
        {
            Guard.Against.NullString(id, Resources.User_InvalidId_ErrorMessage);

            var user = await _userManager.FindByIdAsync(id);

            Guard.Against.NullNotFound(user, string.Format(Resources.User_NotFoundById_ErrorMessage, id));

            return user;
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            Guard.Against.NullString(userName, Resources.User_InvalidUserName_ErrorMessage);

            var user = await _userManager.FindByNameAsync(userName);

            Guard.Against.NullNotFound(user, string.Format(Resources.User_NotFoundByName_ErrorMessage, userName));

            return user;
        }

        public async Task<User> UpdateAsync(string id, User user)
        {
            Guard.Against.NullString(id, Resources.User_InvalidId_ErrorMessage);
            Guard.Against.NullNotValid(user, Resources.User_Invalid_ErrorMessage);

            var result = await FindAsync(id);

            result.FirstName = user.FirstName;
            result.LastName = user.LastName;

            var updateResult = await _userManager.UpdateAsync(result);

            Guard.Against.IsSucceeded(updateResult);

            return result;
        }

        public async Task<IEnumerable<string>> RolesAsync(string id)
        {
            var user = await FindAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            Guard.Against.NullNotFound(roles, Resources.Role_NotFoundList_ErrorMessage);

            return roles;
        }

        public async Task<User> ChangeImageAsync(string id, string imageUrl)
        {
            var user = await FindAsync(id);

            user.ImageUrl = imageUrl;

            var result = await _userManager.UpdateAsync(user);

            Guard.Against.IsSucceeded(result);

            return user;
        }

        public async Task<User> ChangeImageAsync(string id, IFormFile file)
        {
            //await _storageService.CreateImagesContainerAsync();

            //var path = await _storageService.UploadUserImageAsync(id, mimeType, imageStream);

            //return await ChangeImageAsync(id, path);
            return null;
        }

        public async Task<User> DeleteImageAsync(string id)
        {
            //await _storageService.DeleteUserImageAsync(id);

            //return await ChangeImageAsync(id, null);

            return null;
        }

        public async Task<string> PasswordResetTokenAsync(string id)
        {
            var user = await FindAsync(id);

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<string> ChangeEmailTokenAsync(string id, string email)
        {
            Guard.Against.NullString(email, Resources.User_InvalidEmail_ErrorMessage);

            var user = await FindAsync(id);

            if (!user.EmailConfirmed)
            {
                throw new RutrackerException(
                    string.Format(Resources.User_NotConfirmedEmail_ErrorMessage, user.Email),
                    ExceptionEventTypes.InvalidParameters);
            }

            return await _userManager.GenerateChangeEmailTokenAsync(user, email);
        }

        public async Task<string> ChangePhoneNumberTokenAsync(string id, string phone)
        {
            Guard.Against.NullString(phone, message: Resources.User_InvalidPhoneNumber_ErrorMessage);

            var user = await FindAsync(id);

            if (!user.PhoneNumberConfirmed)
            {
                throw new RutrackerException(
                    string.Format(Resources.User_NotConfirmedPhoneNumber_ErrorMessage, user.PhoneNumber),
                    ExceptionEventTypes.InvalidParameters);
            }

            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phone);
        }

        public async Task<User> ResetPasswordAsync(string id, string password, string token)
        {
            var user = await FindAsync(id);
            var result = await _userManager.ResetPasswordAsync(user, token, password);

            Guard.Against.IsSucceeded(result);

            return user;
        }

        public async Task<User> ChangePasswordAsync(string id, string oldPassword, string newPassword)
        {
            Guard.Against.NullString(oldPassword, Resources.User_InvalidOldPassword_ErrorMessage);
            Guard.Against.NullString(newPassword, Resources.User_InvalidNewPassword_ErrorMessage);

            var user = await FindAsync(id);

            if (!await _userManager.CheckPasswordAsync(user, oldPassword))
            {
                throw new RutrackerException(
                    Resources.User_WrongPassword_ErrorMessage,
                    ExceptionEventTypes.InvalidParameters);
            }

            if (oldPassword == newPassword)
            {
                throw new RutrackerException(
                    Resources.User_SamePasswords_ErrorMessage,
                    ExceptionEventTypes.InvalidParameters);
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            Guard.Against.IsSucceeded(result);

            return user;
        }

        public async Task<User> ChangeEmailAsync(string id, string email, string token)
        {
            Guard.Against.NullString(email, Resources.User_InvalidEmail_ErrorMessage);
            Guard.Against.NullString(token, Resources.User_InvalidEmailChangeToken_ErrorMessage);

            var user = await FindAsync(id);
            var result = await _userManager.ChangeEmailAsync(user, email, token);

            Guard.Against.IsSucceeded(result);

            return user;
        }

        public async Task<User> ChangePhoneNumberAsync(string id, string phone, string token)
        {
            Guard.Against.NullString(phone, Resources.User_InvalidEmail_ErrorMessage);
            Guard.Against.NullString(token, Resources.User_InvalidPhoneNumberChangeToken_ErrorMessage);

            var user = await FindAsync(id);
            var result = await _userManager.ChangePhoneNumberAsync(user, phone, token);

            Guard.Against.IsSucceeded(result);

            return user;
        }
    }
}