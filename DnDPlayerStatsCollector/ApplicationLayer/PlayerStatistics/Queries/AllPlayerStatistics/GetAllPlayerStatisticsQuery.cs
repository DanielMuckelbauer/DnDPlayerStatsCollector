using System.Collections.Generic;
using MediatR;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.PlayerStatistics.Queries.AllPlayerStatistics
{
    public class GetAllPlayerStatisticsQuery : IRequest<IEnumerable<PlayerStatisticDto>>
    {
    }
}
