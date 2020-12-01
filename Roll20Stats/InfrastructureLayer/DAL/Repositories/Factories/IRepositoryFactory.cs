using Roll20Stats.InfrastructureLayer.DAL.Models;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.SavingRepositories;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.Factories
{
    public interface IRepositoryFactory
    {
        IReadOnlyRepository<TModel> CreateOnlyRepository<TModel>() where TModel : class, IEntity;
        ISavingRepository<TModel> CreateSavingRepository<TModel>() where TModel : class, IEntity;
    }
}
