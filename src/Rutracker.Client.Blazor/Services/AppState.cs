using System;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.Blazor.Services
{
    public class AppState
    {
        private readonly IUserService _userService;
        private readonly ITorrentService _torrentService;

        public AppState(IUserService userService, ITorrentService torrentService)
        {
            _userService = userService;
            _torrentService = torrentService;
        }

        public bool SearchInProgress { get; private set; }
        public event Action OnChange;


        #region Users

        public async Task<UserViewModel[]> Users()
        {
            return await IndexActionAsync(() => _userService.Users());
        }

        #endregion

        #region Torrents

        public async Task<PaginationResult<TorrentViewModel>> Torrents(int page, int pageSize, FilterViewModel filter)
        {
            return await IndexActionAsync(() => _torrentService.Torrents(page, pageSize, filter));
        }

        public async Task<TorrentDetailsViewModel> Torrent(long id)
        {
            return await IndexActionAsync(() => _torrentService.Torrent(id));
        }

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