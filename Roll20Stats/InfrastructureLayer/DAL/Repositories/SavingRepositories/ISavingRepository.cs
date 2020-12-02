using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.SavingRepositories
{
    public interface ISavingRepository<TModel>
    {
        Task<TModel> GetById(int id);
        Task<TModel> GetSingle(Expression<Func<TModel, bool>> filter);
        void Add(TModel model);
        void Update(TModel model);
        void Remove(TModel model);
    }
}
