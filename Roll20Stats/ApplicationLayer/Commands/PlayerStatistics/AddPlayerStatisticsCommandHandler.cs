using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Roll20Stats.InfrastructureLayer.DAL.Models;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.Factories;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.SavingRepositories;

namespace Roll20Stats.ApplicationLayer.Commands.PlayerStatistics
{
    public class AddPlayerStatisticsCommandHandler : IRequestHandler<AddPlayerStatisticCommand>
    {
        private readonly ISavingRepository<PlayerStatistic> _repository;

        public AddPlayerStatisticsCommandHandler(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.CreateSavingRepository<PlayerStatistic>();
        }

        public Task<Unit> Handle(AddPlayerStatisticCommand request, CancellationToken cancellationToken)
        {
            var playerStatistic = _repository.GetSingle(statistic => statistic.CharacterId == request.CharacterId);
            if (playerStatistic is { })
            {
                playerStatistic.DamageDealt += request.DamageDealt;
                playerStatistic.DamageTaken += request.DamageTaken;
                _repository.Update(playerStatistic);
            }
            else
            {
                var newStatistic = new PlayerStatistic
                {
                    CharacterId = request.CharacterId,
                    DamageDealt = request.DamageDealt,
                    DamageTaken = request.DamageTaken,
                    CharacterName = request.CharacterName
                };
                _repository.Add(newStatistic);
            }

            return Unit.Task;
        }
    }
}