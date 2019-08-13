namespace Rutracker.Client.Blazor
{
    public static class Constants
    {
        public static class Path
        {
            public const string Home = "/";
            public const string Torrents = "/torrents";
            public const string Torrent = "/torrents/{0}";
            public const string Users = "/users";
            public const string Contributors = "/contributors";

            public const string Login = "/account/login";
            public const string Register = "/account/register";
            public const string Profile = "/account/profile";

            public const string Repository = "https://github.com/osipukr/Rutracker-Blazor";
        }
    }
}