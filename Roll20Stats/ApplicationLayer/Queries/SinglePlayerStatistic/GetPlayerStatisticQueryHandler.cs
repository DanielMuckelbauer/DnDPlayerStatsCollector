using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Queries.SinglePlayerStatistic
{
    public class GetPlayerStatisticQueryHandler : IRequestHandler<GetPlayerStatisticQuery, GetPlayerStatisticDto>
    {
        private readonly IApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public GetPlayerStatisticQueryHandler(IApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetPlayerStatisticDto> Handle(GetPlayerStatisticQuery request, CancellationToken cancellationToken)
        {
            var playerStatistic = await _dbContext.PlayerStatistics.SingleOrDefaultAsync(statistic => statistic.CharacterId == request.CharacterId, cancellationToken);
            return playerStatistic is { }
                ? _mapper.Map<GetPlayerStatisticDto>(playerStatistic)
                : default;
        }
    }
}