﻿using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Accounts;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface IAccountViewModelService
    {
        Task<JwtToken> LoginAsync(LoginViewModel model);
        Task<JwtToken> RegisterAsync(RegisterViewModel model);
        Task LogoutAsync();
    }
}