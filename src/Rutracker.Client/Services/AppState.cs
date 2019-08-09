using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.Client.Services.Interfaces;
using Rutracker.Shared.ViewModels.Accounts;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Client.Services
{
    public class AppState : AuthenticationStateProvider
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly ITorrentService _torrentService;

        private JwtToken _token;
        private UserResponseViewModel _user;

        public AppState(IAccountService accountService,
            IUserService userService,
            ITorrentService torrentService)
        {
            _accountService = accountService;
            _userService = userService;
            _torrentService = torrentService;
        }

        public bool SearchInProgress { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public event Action OnChange;

        #region Account

        public async Task Login(LoginViewModel model)
        {
            _token = await _accountService.Login(model);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Register(RegisterViewModel model)
        {
            _token = await _accountService.Register(model);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await _accountService.Logout();

            _token = null;
            _user = null;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        #endregion

        #region Users

        public async Task<IReadOnlyList<UserViewModel>> Users() => await IndexActionAsync(_userService.Users(_token));

        public async Task<UserResponseViewModel> UserDetails()
        {
            if (_user == null || !IsAuthenticated)
            {
                _user = await _userService.UserDetails(_token);
            }

            return _user;
        }

        #endregion

        #region Torrents

        public async Task<TorrentsIndexViewModel> Torrents(int page, int pageSize, FiltrationViewModel filter) => await IndexActionAsync(_torrentService.Torrents(page, pageSize, filter));
        public async Task<TorrentIndexViewModel> Torrent(long id) => await IndexActionAsync(_torrentService.Torrent(id));
        public async Task<FacetViewModel<string>> TitleFacet(int count) => await _torrentService.TitleFacet(count);

        #endregion

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            try
            {
                _user = await UserDetails();

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, _user.User.UserName),
                    new Claim(ClaimTypes.Email, _user.User.Email),
                    new Claim(ClaimTypes.Surname, _user.User.LastName)
                };

                identity = new ClaimsIdentity(claims, "Server authentication");

                IsAuthenticated = true;
            }
            catch (Exception)
            {
                IsAuthenticated = false;
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        #region Helpers

        private async Task<TResult> IndexActionAsync<TResult>(Task<TResult> func)
        {
            SearchInProgress = true;
            NotifyStateChanged();

            try
            {
                return await func;
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