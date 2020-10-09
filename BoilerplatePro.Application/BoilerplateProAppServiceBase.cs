using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using BoilerplatePro.Authorization.Users;
using BoilerplatePro.MultiTenancy;
using BoilerplatePro.Users;
using Microsoft.AspNet.Identity;

namespace BoilerplatePro
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class BoilerplateProAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected BoilerplateProAppServiceBase()
        {
            LocalizationSourceName = BoilerplateProConsts.LocalizationSourceName;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}