using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Roll20Stats.InfrastructureLayer.DAL.Entities;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.Factories;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.SavingRepositories;

namespace Roll20Stats.ApplicationLayer.Commands.DeletePlayerStatistic
{
    public class DeletePlayerStatisticCommandHandler : IRequestHandler<DeletePlayerStatisticCommand>
    {
        private readonly ISavingRepository<PlayerStatistic> _savingRepository;

        public DeletePlayerStatisticCommandHandler(IRepositoryFactory repositoryFactory)
        {
            _savingRepository = repositoryFactory.CreateSavingRepository<PlayerStatistic>();
        }

        public async Task<Unit> Handle(DeletePlayerStatisticCommand request, CancellationToken cancellationToken)
        {
            var playerStatistic =
                await _savingRepository.GetSingle(statistic => statistic.CharacterId == request.CharacterId);
            _savingRepository.Remove(playerStatistic);
            return Unit.Value;
        }
    }
}
