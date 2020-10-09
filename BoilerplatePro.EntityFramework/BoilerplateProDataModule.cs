using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using BoilerplatePro.EntityFramework;

namespace BoilerplatePro
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(BoilerplateProCoreModule))]
    public class BoilerplateProDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<BoilerplateProDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
