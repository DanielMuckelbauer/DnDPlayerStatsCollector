using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository
{
    public interface IReadOnlyRepository<TModel>
    {
        IQueryable<TModel> GetAll();
        Task<TModel> GetSingle(Expression<Func<TModel, bool>> filter);
    }
}
