using System.Threading.Tasks;
using Abp.Application.Services;
using BoilerplatePro.Configuration.Dto;

namespace BoilerplatePro.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}