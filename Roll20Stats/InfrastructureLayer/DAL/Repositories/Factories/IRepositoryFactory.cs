using Roll20Stats.InfrastructureLayer.DAL.Entities;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.SavingRepositories;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.Factories
{
    public interface IRepositoryFactory
    {
        IReadOnlyRepository<TModel> CreateReadOnlyRepository<TModel>() where TModel : class, IEntity;
        ISavingRepository<TModel> CreateSavingRepository<TModel>() where TModel : class, IEntity;
    }
}
