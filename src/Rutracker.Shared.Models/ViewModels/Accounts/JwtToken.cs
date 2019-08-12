namespace Rutracker.Shared.Models.ViewModels.Accounts
{
    public class JwtToken
    {
        public string Token { get; set; }
        public long ExpiresIn { get; set; }
    }
}