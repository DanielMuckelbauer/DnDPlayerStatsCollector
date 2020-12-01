using System.Linq;
using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Context;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository
{
    public class ReadOnlyRepository<TModel> : IReadOnlyRepository<TModel> where TModel : class
    {
        private readonly IApplicationContext _applicationContext;
        private readonly DbSet<TModel> _dbSet;

        public ReadOnlyRepository(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _dbSet = applicationContext.Set<TModel>();
        }

        public IQueryable<TModel> QueryAll()
        {
            throw new System.NotImplementedException();
        }
    }
}