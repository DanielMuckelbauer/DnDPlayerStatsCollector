using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Roll20Stats.ApplicationLayer.DataTransferObjects;
using Roll20Stats.InfrastructureLayer.DAL.Entities;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.Factories;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository;

namespace Roll20Stats.ApplicationLayer.Queries.SinglePlayerStatistic
{
    public class GetPlayerStatisticQueryHandler : IRequestHandler<GetPlayerStatisticQuery, PlayerStatisticDTO>
    {
        private readonly IReadOnlyRepository<PlayerStatistic> _repository;
        private readonly IMapper _mapper;

        public GetPlayerStatisticQueryHandler(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repository = repositoryFactory.CreateReadOnlyRepository<PlayerStatistic>();
            _mapper = mapper;
        }

        public async Task<PlayerStatisticDTO> Handle(GetPlayerStatisticQuery request, CancellationToken cancellationToken)
        {
            var playerStatistic = await _repository.GetSingle(statistic => statistic.CharacterId == request.CharacterId);
            return playerStatistic is { }
                ? _mapper.Map<PlayerStatisticDTO>(playerStatistic)
                : default;
        }
    }
}