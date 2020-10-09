using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using BoilerplatePro.EntityFramework;

namespace BoilerplatePro.Migrator
{
    [DependsOn(typeof(BoilerplateProDataModule))]
    public class BoilerplateProMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<BoilerplateProDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}