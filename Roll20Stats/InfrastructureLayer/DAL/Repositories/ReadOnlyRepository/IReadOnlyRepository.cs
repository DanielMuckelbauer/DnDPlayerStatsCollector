using System;
using System.Linq;
using System.Linq.Expressions;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository
{
    public interface IReadOnlyRepository<TModel>
    {
        IQueryable<TModel> QueryAll();
        TModel GetSingle(Expression<Func<TModel, bool>> filter);
    }
}
