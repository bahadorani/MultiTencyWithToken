using Sample.Application.Services;
using Sample.Domain.Models;

namespace Sample.Application.Interface;

public interface ITenantSetter
{
    void SetTenant(Tenant key);
}