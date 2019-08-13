﻿using System;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Shared.Models.ViewModels.Accounts;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.Torrents;
using Rutracker.Shared.Models.ViewModels.Users;

namespace Rutracker.Client.Blazor.Services
{
    public class AppState
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly ITorrentService _torrentService;

        private readonly ApiAuthenticationStateProvider _apiAuthenticationState;

        public AppState(
            IAccountService accountService,
            IUserService userService,
            ITorrentService torrentService,
            ApiAuthenticationStateProvider apiAuthenticationState)
        {
            _accountService = accountService;
            _userService = userService;
            _torrentService = torrentService;
            _apiAuthenticationState = apiAuthenticationState;
        }

        public bool SearchInProgress { get; private set; }
        public event Action OnChange;

        #region Account

        public async Task Login(LoginViewModel model)
        {
            var token = await _accountService.Login(model);

            await _apiAuthenticationState.MarkUserAsAuthenticated(token.Token);
        }

        public async Task Register(RegisterViewModel model)
        {
            var token = await _accountService.Register(model);

            await _apiAuthenticationState.MarkUserAsAuthenticated(token.Token);

        }

        public async Task Logout()
        {
            await _accountService.Logout();
            await _apiAuthenticationState.MarkUserAsLoggedOut();
        }

        #endregion

        #region Users

        public async Task<UserViewModel[]> Users() => await IndexActionAsync(() => _userService.Users());
        public async Task<UserViewModel> UserDetails() => await IndexActionAsync(() => _userService.UserDetails());
        public async Task UpdateUser(EditUserViewModel model) => await _userService.UpdateUser(model);

        #endregion

        #region Torrents

        public async Task<TorrentsIndexViewModel> Torrents(int page, int pageSize, FiltrationViewModel filter) => await IndexActionAsync(() => _torrentService.Torrents(page, pageSize, filter));
        public async Task<TorrentIndexViewModel> Torrent(long id) => await IndexActionAsync(() => _torrentService.Torrent(id));
        public async Task<FacetViewModel<string>> TitleFacet(int count) => await _torrentService.TitleFacet(count);

        #endregion

        #region Helpers

        private async Task<TResult> IndexActionAsync<TResult>(Func<Task<TResult>> func)
        {
            SearchInProgress = true;
            NotifyStateChanged();

            try
            {
                return await func();
            }
            finally
            {
                SearchInProgress = false;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        #endregion
    }
}