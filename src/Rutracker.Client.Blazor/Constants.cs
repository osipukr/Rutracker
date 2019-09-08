﻿namespace Rutracker.Client.Blazor
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
            public const string Users = "/users";
            public const string User = "/users/{0}";
            public const string Contributors = "/contributors";

            public const string Login = "/account/login";
            public const string Register = "/account/register";
            public const string Logout = "/account/logout";
            public const string Profile = "/account/profile";
            public const string ConfirmEmail = "/account/confirm_email/{0}/{1}";
            public const string ConfirmChangeEmail = "/account/confirm_change_email/{0}/{1}";

            public const string Repository = "https://github.com/osipukr/Rutracker-Blazor";
        }

        public static class File
        {
            public const long MaxImageSize = 300 * 1024;
        }
    }
}