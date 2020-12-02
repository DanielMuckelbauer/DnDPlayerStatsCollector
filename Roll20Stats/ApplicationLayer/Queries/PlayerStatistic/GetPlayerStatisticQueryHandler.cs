using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.Factories;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository;

namespace Roll20Stats.ApplicationLayer.Queries.PlayerStatistic
{
    public class GetPlayerStatisticQueryHandler : IRequestHandler<GetPlayerStatisticQuery, PlayerStatisticDTO>
    {
        private readonly IReadOnlyRepository<InfrastructureLayer.DAL.Models.PlayerStatistic> _repository;
        private readonly IMapper _mapper;

        public GetPlayerStatisticQueryHandler(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repository = repositoryFactory.CreateReadOnlyRepository<InfrastructureLayer.DAL.Models.PlayerStatistic>();
            _mapper = mapper;
        }

        public Task<PlayerStatisticDTO> Handle(GetPlayerStatisticQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetSingle(statistic => statistic.CharacterId == request.CharacterId) is { } playerStatistic
                ? Task.FromResult(_mapper.Map<PlayerStatisticDTO>(playerStatistic))
                : Task.FromResult(default(PlayerStatisticDTO))!;
        }
    }
}