using System.ComponentModel.DataAnnotations;

namespace Rutracker.Server.BusinessLayer.Extensions
{
    public static class ValidatorExtensions
    {
        public static bool IsValidEmail(this string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        public static bool IsValidPhoneNumber(this string phone)
        {
            return new PhoneAttribute().IsValid(phone);
        }

        public static bool IsValidUrl(this string url)
        {
            return new UrlAttribute().IsValid(url);
        }
    }
}