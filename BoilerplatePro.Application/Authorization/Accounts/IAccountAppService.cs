using System.Threading.Tasks;
using Abp.Application.Services;
using BoilerplatePro.Authorization.Accounts.Dto;

namespace BoilerplatePro.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
