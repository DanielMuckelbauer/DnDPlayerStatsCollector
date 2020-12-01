using System;
using System.Linq.Expressions;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.SavingRepositories
{
    public interface ISavingRepository<TModel>
    {
        TModel GetById(int id);
        TModel GetSingle(Expression<Func<TModel, bool>> filter);
        void Add(TModel model);
        void Update(TModel model);
        void Remove(TModel model);
    }
}
