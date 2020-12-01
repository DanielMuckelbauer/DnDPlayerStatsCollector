using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Roll20Stats.InfrastructureLayer.DAL.Models;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.Factories;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository;

namespace Roll20Stats.ApplicationLayer.Queries.PlayerStatistics
{
    public class GetPlayerStatisticQueryHandler : IRequestHandler<GetPlayerStatisticQuery, PlayerStatisticDTO>
    {
        private readonly IReadOnlyRepository<PlayerStatistic> _repository;

        public GetPlayerStatisticQueryHandler(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.CreateReadOnlyRepository<PlayerStatistic>();
        }

        public Task<PlayerStatisticDTO> Handle(GetPlayerStatisticQuery request, CancellationToken cancellationToken)
        {
            var playerStatistic = _repository.GetSingle(statistic => statistic.CharacterId == request.CharacterId);
            return playerStatistic is {}
                ? Task.FromResult(new PlayerStatisticDTO
                {
                    DamageDealt = playerStatistic.DamageDealt,
                    CharacterId = playerStatistic.CharacterId,
                    CharacterName = playerStatistic.CharacterName,
                    DamageTaken = playerStatistic.DamageTaken
                })
                : Task.FromResult(default(PlayerStatisticDTO));
        }
    }
}