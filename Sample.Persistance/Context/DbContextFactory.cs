using Microsoft.EntityFrameworkCore;
using Sample.Domain.Models;

namespace Sample.Persistence.Context
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly DbContextOptionsBuilder<MultiTenantContext> _optionsBuilder;

        public DbContextFactory()
        {
            _optionsBuilder = new DbContextOptionsBuilder<MultiTenantContext>();
        }

        public MultiTenantContext CreateDbContext(Tenant tenant)
        {
            _optionsBuilder.UseSqlServer(tenant.ConnectionString);
            return new MultiTenantContext(_optionsBuilder.Options);
        }
    }
}

