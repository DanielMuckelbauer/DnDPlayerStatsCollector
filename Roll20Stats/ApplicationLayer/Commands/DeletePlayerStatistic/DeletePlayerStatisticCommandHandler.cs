using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Context;

namespace Roll20Stats.ApplicationLayer.Commands.DeletePlayerStatistic
{
    public class DeletePlayerStatisticCommandHandler : IRequestHandler<DeletePlayerStatisticCommand>
    {
        private readonly IApplicationContext _dbContext;

        public DeletePlayerStatisticCommandHandler(IApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeletePlayerStatisticCommand request, CancellationToken cancellationToken)
        {
            var playerStatistic = await _dbContext.PlayerStatistics
                .SingleOrDefaultAsync(statistic => statistic.CharacterId == request.CharacterId, cancellationToken);
            _dbContext.PlayerStatistics.Remove(playerStatistic);
            _dbContext.SaveChanges();
            return Unit.Value;
        }
    }
}
