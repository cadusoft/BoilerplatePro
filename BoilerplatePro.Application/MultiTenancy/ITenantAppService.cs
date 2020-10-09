using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BoilerplatePro.MultiTenancy.Dto;

namespace BoilerplatePro.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
