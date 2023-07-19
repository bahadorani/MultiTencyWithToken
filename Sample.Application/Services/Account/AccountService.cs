using Sample.Application.Services;
using Sample.Domain.ViewModels;

namespace Domino.Application
{
    public class AccountService:IAccountService
    {
        private readonly IUserService _userService;

        public AccountService(IUserService userService)
        {
            _userService = userService;
        }

        public TokenRequest? GetTokenRequest(LoginViewModel model)
        {

            var userCheckResult = _userService.AuthenticateUser(model.UserName);
            if (userCheckResult.code.HasValue)
            {
                return null;
            }

            var userData = userCheckResult.user;

            var tokenRequest = new TokenRequest()
            {
                UserId = userData.Id.ToString(),
                UserName = userData.Name,

            };

            return tokenRequest;
        }
    }
}
