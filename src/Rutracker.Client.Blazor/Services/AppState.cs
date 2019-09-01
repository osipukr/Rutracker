using System;
using System.Collections.Generic;
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
        private readonly ICategoryService _categoryService;

        public AppState(IUserService userService, ITorrentService torrentService, ICategoryService categoryService)
        {
            _userService = userService;
            _torrentService = torrentService;
            _categoryService = categoryService;
        }

        public bool SearchInProgress { get; private set; }
        public event Action OnChange;


        public async Task<UserViewModel[]> Users()
        {
            return await IndexActionAsync(() => _userService.Users());
        }

        public async Task<IEnumerable<CategoryViewModel>> Categories()
        {
            return await IndexActionAsync(() => _categoryService.ListAsync());
        }

        public async Task<CategoryViewModel> Category(int id)
        {
            return await IndexActionAsync(() => _categoryService.FindAsync(id));
        }

        public async Task<PaginationResult<TorrentViewModel>> Torrents(int page, int pageSize, FilterViewModel filter)
        {
            return await IndexActionAsync(() => _torrentService.Torrents(page, pageSize, filter));
        }

        public async Task<IEnumerable<TorrentViewModel>> Popular(int count)
        {
            return await IndexActionAsync(() => _torrentService.Popular(count));
        }

        public async Task<TorrentDetailsViewModel> Torrent(int id)
        {
            return await IndexActionAsync(() => _torrentService.Torrent(id));
        }

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
    }
}