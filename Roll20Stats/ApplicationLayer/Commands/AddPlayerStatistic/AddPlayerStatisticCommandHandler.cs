using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.InfrastructureLayer.DAL.Entities;

namespace Roll20Stats.ApplicationLayer.Commands.AddPlayerStatistic
{
    public class AddPlayerStatisticCommandHandler : IRequestHandler<AddPlayerStatisticCommand>
    {
        private readonly IApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public AddPlayerStatisticCommandHandler(IApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddPlayerStatisticCommand request, CancellationToken cancellationToken)
        {
            if (await _dbContext.PlayerStatistics.SingleOrDefaultAsync(statistic => statistic.CharacterId == request.CharacterId, cancellationToken) is { } playerStatistic)
            {
                playerStatistic.DamageDealt += request.DamageDealt;
                playerStatistic.DamageTaken += request.DamageTaken;
                _dbContext.PlayerStatistics.Update(playerStatistic);
            }
            else
            {
                await _dbContext.PlayerStatistics.AddAsync(_mapper.Map<PlayerStatistic>(request), cancellationToken);
            }

            _dbContext.SaveChanges();

            return Unit.Value;
        }
    }
}