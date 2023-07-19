
using Sample.Domain.Models;

namespace Sample.Application.Services
{
    public interface ITenantService
    {
        void SetTenant(Tenant tenant);
    }
}
