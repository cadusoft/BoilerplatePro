using Abp.Authorization;
using BoilerplatePro.Authorization.Roles;
using BoilerplatePro.Authorization.Users;

namespace BoilerplatePro.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
