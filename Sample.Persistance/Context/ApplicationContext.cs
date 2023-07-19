using Microsoft.EntityFrameworkCore;
using Sample.Domain.Models;

namespace Sample.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = default!;
    }
}
