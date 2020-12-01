using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.InfrastructureLayer.DAL.Models;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.SavingRepositories;

namespace Roll20Stats.InfrastructureLayer.DAL.Repositories.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IApplicationContext _applicationContext;

        public RepositoryFactory(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IReadOnlyRepository<TModel> CreateReadOnlyRepository<TModel>() where TModel : class, IEntity
        {
            return new ReadOnlyRepository<TModel>(_applicationContext);
        }

        public ISavingRepository<TModel> CreateSavingRepository<TModel>() where TModel : class, IEntity
        {
            return new SavingRepository<TModel>(_applicationContext);
        }
    }
}