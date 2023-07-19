using Sample.Common.CustomError;
using Sample.Domain.Models;
using Sample.Persistence.Context;
using System.ComponentModel.DataAnnotations;

namespace Sample.Application.Services
{
    public class UserService : IUserService
    {
        private ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
            SeedData();
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public (User? user, int? code) AuthenticateUser(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return (null, null);

            var authenticationCheckResult = IsUserAuthenticated(userName);
            if (!authenticationCheckResult.isAuthenticated) return (null, (int)CustomHttpStatusCodes.Unauthorized);

            return (authenticationCheckResult.user, null);
        }

        private (User? user, bool isAuthenticated) IsUserAuthenticated(string userName)
        {
            User? user = FindUserByName(userName);
            if (user == null) return (null, false);

            return (user, true);
        }

        private User? FindUserByName(string name)
        {
            name = name.ToLower();
            var user = new User
            {
                Id = 1,
                Name = name,
                Email = "test@yahoo.com"
            };
            return user;
        }

        private void SeedData()
        {
            if (_context.Users.Count() == 0)
            {
                var user = new User()
                {
                    Name = "Bahadorani",
                    Email = "sample@yahoo.com"
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }
    }
}
