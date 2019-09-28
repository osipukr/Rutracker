namespace Rutracker.Server.WebApi.Settings
{
    public class ClientSettings
    {
        public string BaseUrl { get; set; }
        public string CompleteRegistrationPath { get; set; }
        public string EmailChangeConfirmPath { get; set; }
        public string ResetPasswordPath { get; set; }
    }
}