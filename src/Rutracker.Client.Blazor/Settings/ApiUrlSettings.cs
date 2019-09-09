namespace Rutracker.Client.Blazor.Settings
{
    public class ApiUrlSettings
    {
        // Torrent
        public string Categories { get; set; }
        public string Category { get; set; }
        public string Subcategories { get; set; }
        public string SubcategoriesList{ get; set; }
        public string Subcategory { get; set; }
        public string Torrents { get; set; }
        public string PopularTorrents { get; set; }
        public string Torrent { get; set; }
        public string Comments { get; set; }
        public string CommentsPagination { get; set; }
        public string Comment { get; set; }
        public string LikeComment { get; set; }

        // Account
        public string Login { get; set; }
        public string Register { get; set; }
        public string Logout { get; set; }

        // User
        public string Users { get; set; }
        public string UserProfile { get; set; }
        public string User { get; set; }
        public string ChangeUserInfo { get; set; }
        public string ChangeImage { get; set; }
        public string ChangePassword { get; set; }
        public string ChangeEmail { get; set; }
        public string ChangePhone { get; set; }
        public string ConfirmEmail { get; set; }
        public string ConfirmPhone { get; set; }
        public string ConfirmChangeEmail { get; set; }
    }
}