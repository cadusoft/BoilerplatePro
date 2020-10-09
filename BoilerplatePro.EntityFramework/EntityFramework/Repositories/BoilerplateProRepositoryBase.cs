using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace BoilerplatePro.EntityFramework.Repositories
{
    public abstract class BoilerplateProRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<BoilerplateProDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected BoilerplateProRepositoryBase(IDbContextProvider<BoilerplateProDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class BoilerplateProRepositoryBase<TEntity> : BoilerplateProRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected BoilerplateProRepositoryBase(IDbContextProvider<BoilerplateProDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
