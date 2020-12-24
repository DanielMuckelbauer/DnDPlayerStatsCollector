using AutoMapper;
using MediatR;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.PresentationLayer.DataTransferObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Roll20Stats.ApplicationLayer.PlayerStatistics.Queries.AllPlayerStatistics
{
    public class GetAllPlayerStatisticsQueryHandler : IRequestHandler<GetAllPlayerStatisticsQuery, IEnumerable<GetPlayerStatisticDto>>
    {
        private readonly IApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllPlayerStatisticsQueryHandler(IApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<IEnumerable<GetPlayerStatisticDto>> Handle(GetAllPlayerStatisticsQuery request, CancellationToken _)
        {
            return Task.FromResult(_mapper.Map<IEnumerable<GetPlayerStatisticDto>>(_dbContext.PlayerStatistics));
        }
    }
}
