using Sample.Domain.Models;

namespace Sample.Persistence.Context
{
    public interface IDbContextFactory
    {
        MultiTenantContext CreateDbContext(Tenant tenant);
    }
}