using Sample.Domain.Models;

namespace Sample.Application.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        (User? user, int? code) AuthenticateUser(string userName);
    }
}
