using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using BoilerplatePro.Configuration.Dto;

namespace BoilerplatePro.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BoilerplateProAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
