using System.Collections.Generic;
using MediatR;
using Roll20Stats.ApplicationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Queries.AllPlayerStatistics
{
    public class GetAllPlayerStatisticsQuery : IRequest<IEnumerable<PlayerStatisticDTO>>
    {
    }
}
