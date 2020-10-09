using BoilerplatePro.EntityFramework;
using EntityFramework.DynamicFilters;

namespace BoilerplatePro.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly BoilerplateProDbContext _context;

        public InitialHostDbBuilder(BoilerplateProDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
