using Sample.Application.Interface;
using Sample.Domain.Models;

namespace Sample.Application.Services;
public class TenantService : ITenantGetter, ITenantSetter, ITenantService
{
    public Tenant Tenant { get; private set; } = default!;

    public void SetTenant(Tenant tenant)
    {
        Tenant = tenant;
    }
}