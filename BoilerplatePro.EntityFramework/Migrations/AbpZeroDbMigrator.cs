using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using BoilerplatePro.EntityFramework;

namespace BoilerplatePro.Migrations
{
    public class AbpZeroDbMigrator : AbpZeroDbMigrator<BoilerplateProDbContext, Configuration>
    {
        public AbpZeroDbMigrator(
            IUnitOfWorkManager unitOfWorkManager,
            IDbPerTenantConnectionStringResolver connectionStringResolver,
            IIocResolver iocResolver)
            : base(
                unitOfWorkManager,
                connectionStringResolver,
                iocResolver)
        {
        }
    }
}
