using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Context;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.SavingRepositories
{
    public class SavingRepository<TModel> : ISavingRepository<TModel> where TModel : class
    {
        private readonly IApplicationContext _applicationContext;
        private DbSet<TModel> _dbSet;

        public SavingRepository(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _dbSet = applicationContext.Set<TModel>();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public TModel GetByCharacterId(string characterId)
        {
            throw new System.NotImplementedException();
        }

        public void Add(TModel model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TModel model)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(TModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}