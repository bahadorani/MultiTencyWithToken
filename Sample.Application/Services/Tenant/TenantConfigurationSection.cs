using Sample.Domain.Models;

namespace Sample.Application.Services;

public class TenantConfigurationSection
{
    public List<Tenant> Tenants { get; set; }
}