namespace Rutracker.Client.BlazorWasm
{
    public static class Constants
    {
        public static class Path
        {
            public const string Home = "/";
            public const string Categories = "/categories";
            public const string Category = "/categories/{0}";
            public const string Torrents = "/torrents";
            public const string PopularTorrents = "/torrents/popular";
            public const string Torrent = "/torrents/{0}";
            public const string TorrentCreate = "/torrents/create";
            public const string Users = "/users";
            public const string User = "/users/{0}";
            public const string Contributors = "/contributors";

            public const string Login = "/account/login";
            public const string Register = "/account/register";
            public const string Logout = "/account/logout";
            public const string Profile = "/account/profile";

            public const string Repository = "https://github.com/osipukr/Rutracker-Blazor";
            public const string DefaultUserImage = "https://icon-library.net/images/no-profile-pic-icon/no-profile-pic-icon-5.jpg";
            public const string DefaultTorrentImage = "https://www.southtabor.com/newsite/wp-content/themes/consultix/images/no-image-found-360x250.png";
        }

        public static class File
        {
            public const long MaxImageSize = 2 * 1024 * 1024;
            public const long MaxFileSize = 50 * 1024 * 1024;
        }
    }
}