namespace Rutracker.Client.Host.Options
{
    public class ApiOptions
    {
        // Category
        public string Categories { get; set; }
        public string Category { get; set; }

        // Subcategory
        public string Subcategories { get; set; }
        public string Subcategory { get; set; }

        // Torrent
        public string Torrents { get; set; }
        public string Torrent { get; set; }

        // File
        public string Files { get; set; }
        public string File { get; set; }

        // Comment
        public string Comments { get; set; }
        public string Comment { get; set; }
        public string CommentLike { get; set; }

        // Account
        public string Login { get; set; }
        public string Register { get; set; }
        public string Logout { get; set; }

        // User
        public string Users { get; set; }
        public string User { get; set; }
        public string UserProfile { get; set; }
        public string ChangeUserInfo { get; set; }
        public string ChangePassword { get; set; }
        public string ForgotPassword { get; set; }
        public string ResetPassword { get; set; }
    }
}