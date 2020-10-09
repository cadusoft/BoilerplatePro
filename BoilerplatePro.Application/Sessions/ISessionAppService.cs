using System.Threading.Tasks;
using Abp.Application.Services;
using BoilerplatePro.Sessions.Dto;

namespace BoilerplatePro.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
