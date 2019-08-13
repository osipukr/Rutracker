namespace Rutracker.Shared.Models.ViewModels.Account
{
    public class JwtToken
    {
        public string Token { get; set; }
        public long ExpiresIn { get; set; }
    }
}