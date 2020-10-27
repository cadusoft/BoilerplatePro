using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using BoilerplatePro.Authorization;

namespace BoilerplatePro.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class BoilerplateProNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "home",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Customers,
                        L("Customers"),
                        url: "Customers",
                        icon: "people"
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Endpoints,
                        L("Endpoints"),
                        url: "Endpoints",
                        icon: "dns"
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Transfers,
                        L("Transfers"),
                        url: "Transfers/TransfersByEntity",
                        icon: "playlist_add_check"
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "people",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BoilerplateProConsts.LocalizationSourceName);
        }
    }
}
