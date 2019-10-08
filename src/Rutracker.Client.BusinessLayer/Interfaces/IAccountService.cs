using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Client.BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task Login(LoginViewModel model);
        Task Register(RegisterViewModel model);
        Task CompleteRegistration(CompleteRegistrationViewModel model);
        Task ForgotPassword(ForgotPasswordViewModel model);
        Task ResetPassword(ResetPasswordViewModel model);
        Task Logout();
    }
}