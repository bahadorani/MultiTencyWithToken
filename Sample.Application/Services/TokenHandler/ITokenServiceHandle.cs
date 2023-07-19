using Sample.Domain.ViewModels;

namespace Sample.Application.Services.Handler
{
    public interface ITokenServiceHandle
    {
        Task<string> GenerateTokenAsync(TokenRequest model);
        public int? ValidateToken(string token);

    }
}
