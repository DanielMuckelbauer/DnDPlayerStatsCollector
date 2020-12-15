using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Queries.SinglePlayerStatistic
{
    public class GetPlayerStatisticQueryHandler : IRequestHandler<GetPlayerStatisticQuery, ResponseWithMetaData<GetPlayerStatisticRequest>>
    {
        private readonly IApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public GetPlayerStatisticQueryHandler(IApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResponseWithMetaData<GetPlayerStatisticRequest>> Handle(GetPlayerStatisticQuery request, CancellationToken cancellationToken)
        {
            var playerStatistic = await _dbContext.PlayerStatistics.SingleOrDefaultAsync(statistic => statistic.CharacterId == request.CharacterId, cancellationToken);
            return playerStatistic is { }
                ? _mapper.Map<ResponseWithMetaData<GetPlayerStatisticRequest>>(playerStatistic)
                : new ResponseWithMetaData<GetPlayerStatisticRequest>
                {
                    HasError = true,
                    StatusCode = 404,
                    ErrorMessage = $@"Player with id ""{request.CharacterId}"" was not found."
                };
        }
    }
}