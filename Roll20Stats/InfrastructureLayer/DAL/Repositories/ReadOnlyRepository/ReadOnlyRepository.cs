using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.InfrastructureLayer.DAL.Entities;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository
{
    public class ReadOnlyRepository<TModel> : IReadOnlyRepository<TModel> where TModel : class, IEntity
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
            return _dbSet;
        }

        public Task<TModel> GetSingle(Expression<Func<TModel, bool>> filter)
            => _dbSet.SingleOrDefaultAsync(filter);
    }
}