namespace Rutracker.Client.BusinessLayer.Settings
{
    public class ApiUrlOptions
    {
        // Category
        public string Categories { get; set; }
        public string Category { get; set; }

        // Subcategory
        public string Subcategories { get; set; }
        public string SubcategoriesSearch { get; set; }
        public string Subcategory { get; set; }

        // Torrent
        public string Torrents { get; set; }
        public string TorrentsSearch { get; set; }
        public string PopularTorrentsSearch { get; set; }
        public string Torrent { get; set; }
        public string TorrentImage { get; set; }


        // File
        public string Files { get; set; }
        public string FilesSearch { get; set; }
        public string File { get; set; }
        public string FileDownload { get; set; }

        // Comment
        public string Comments { get; set; }
        public string CommentsSearch { get; set; }
        public string Comment { get; set; }
        public string LikeComment { get; set; }

        // Account
        public string Login { get; set; }
        public string Register { get; set; }
        public string CompleteRegistration { get; set; }
        public string ForgotPassword { get; set; }
        public string ResetPassword { get; set; }
        public string Logout { get; set; }

        // User
        public string UsersSearch { get; set; }
        public string UserProfile { get; set; }
        public string User { get; set; }
        public string ChangeUserInfo { get; set; }
        public string ChangeImage { get; set; }
        public string ChangePassword { get; set; }
        public string ChangeEmail { get; set; }
        public string ChangePhone { get; set; }
        public string ConfirmPhone { get; set; }
        public string ConfirmChangeEmail { get; set; }
    }
}