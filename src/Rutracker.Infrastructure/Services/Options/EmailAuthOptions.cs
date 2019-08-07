namespace Rutracker.Infrastructure.Services.Options
{
    public class EmailAuthOptions
    {
        public string SmtpServer { get; set; }
        public int SmtpServerPort { get; set; }
        public string SenderEmail { get; set; }
        public string SenderEmailPassword { get; set; }
    }
}