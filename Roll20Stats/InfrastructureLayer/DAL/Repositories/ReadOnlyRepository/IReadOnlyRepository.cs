using System.Linq;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository
{
    public interface IReadOnlyRepository<TModel>
    {
        IQueryable<TModel> QueryAll();
    }
}
