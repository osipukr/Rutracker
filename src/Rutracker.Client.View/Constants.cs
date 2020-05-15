using Skclusive.Core.Component;

namespace Rutracker.Client.View
{
    public static class Constants
    {
        public const string BrandName = "Rutracker";

        public static class Page
        {
            public const string Home = "/";
            public const string SignIn = "/signin";
            public const string SignUp = "/signup";
            public const string SignOut = "/signout";
            public const string ForgotPassword = "/forgotPassword";
            public const string ResetPassword = "/resetPassword";
            public const string Account = "/account";
            public const string Categories = "/categories";
            public const string Category = "/categories/{0}";
            public const string Torrents = "/torrents";
            public const string Torrent = "/torrents/{0}";
            public const string Users = "/users";
            public const string User = "/users/{0}";
            public const string Settings = "/settings";
        }

        public static class Filter
        {
            public const int PageIndexFrom = 1;
            public const int DefaultPage = 1;
            public const int PageSize = 20;
        }
    }
}