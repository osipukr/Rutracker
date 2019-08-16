namespace Rutracker.Shared.Models
{
    public class JwtToken
    {
        public string Token { get; set; }
        public long ExpiresIn { get; set; }
    }
}