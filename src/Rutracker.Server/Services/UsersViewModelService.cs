using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Server.Services
{
    public class UsersViewModelService : IUsersViewModelService
    {
        public UsersViewModelService()
        {
            
        }

        public async Task<UserViewModel> GetUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}