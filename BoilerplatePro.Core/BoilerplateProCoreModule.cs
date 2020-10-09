using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using BoilerplatePro.Authorization;
using BoilerplatePro.Authorization.Roles;
using BoilerplatePro.Authorization.Users;
using BoilerplatePro.Configuration;
using BoilerplatePro.MultiTenancy;

namespace BoilerplatePro
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class BoilerplateProCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Remove the following line to disable multi-tenancy.
            Configuration.MultiTenancy.IsEnabled = BoilerplateProConsts.MultiTenancyEnabled;

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    BoilerplateProConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "BoilerplatePro.Localization.Source"
                        )
                    )
                );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<BoilerplateProAuthorizationProvider>();

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
