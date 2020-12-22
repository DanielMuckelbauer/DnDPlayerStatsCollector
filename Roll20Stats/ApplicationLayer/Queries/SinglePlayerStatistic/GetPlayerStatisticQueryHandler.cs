using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Queries.SinglePlayerStatistic
{
    public class GetPlayerStatisticQueryHandler : IRequestHandler<GetPlayerStatisticQuery, ResponseWithMetaData<GetPlayerStatisticDto>>
    {
        private readonly IApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public GetPlayerStatisticQueryHandler(IApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResponseWithMetaData<GetPlayerStatisticDto>> Handle(GetPlayerStatisticQuery request, CancellationToken _)
        {
            var playerStatistic = await _dbContext.PlayerStatistics
                .Include(statistic => statistic.Game)
                .SingleOrDefaultAsync(statistic => statistic.CharacterId == request.CharacterId && statistic.Game.Name == request.GameName);
            return playerStatistic is { }
                ? _mapper.Map<ResponseWithMetaData<GetPlayerStatisticDto>>(playerStatistic)
                : new ResponseWithMetaData<GetPlayerStatisticDto>
                {
                    HasError = true,
                    StatusCode = 404,
                    ErrorMessage = $@"Player with id ""{request.CharacterId}"" and game name ""{request.GameName}"" was not found."
                };
        }
    }
}