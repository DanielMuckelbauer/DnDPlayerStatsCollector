using System.Linq;
using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.InfrastructureLayer.DAL.Models;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.SavingRepositories
{
    public class SavingRepository<TModel> : ISavingRepository<TModel> where TModel : class, IEntity
    {
        private readonly IApplicationContext _applicationContext;
        private readonly DbSet<TModel> _dbSet;

        public SavingRepository(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _dbSet = applicationContext.Set<TModel>();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public TModel GetById(int id) 
            => _dbSet.FirstOrDefault(entry => entry.Id == id);

        public void Add(TModel model)
        {
            _dbSet.Add(model);
            _applicationContext.SaveChanges();
        }

        public void Update(TModel model)
        {
            _dbSet.Update(model);
            _applicationContext.SaveChanges();
        }

        public void Remove(TModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}