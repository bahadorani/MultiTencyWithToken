using Sample.Domain.ViewModels;

namespace Domino.Application
{
    public interface IAccountService
    {
        TokenRequest?  GetTokenRequest(LoginViewModel model);
    }
}
