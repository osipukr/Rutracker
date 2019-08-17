namespace Rutracker.Server.BusinessLayer.Options
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpServerPort { get; set; }
        public string SenderEmail { get; set; }
        public string SenderEmailPassword { get; set; }
    }
}