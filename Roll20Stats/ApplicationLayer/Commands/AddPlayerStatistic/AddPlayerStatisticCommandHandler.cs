using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.InfrastructureLayer.DAL.Entities;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Commands.AddPlayerStatistic
{
    public class AddPlayerStatisticCommandHandler : IRequestHandler<AddPlayerStatisticCommand, ResponseWithMetaData<AddPlayerStatisticDto>>
    {
        private readonly IApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public AddPlayerStatisticCommandHandler(IApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResponseWithMetaData<AddPlayerStatisticDto>> Handle(AddPlayerStatisticCommand request, CancellationToken cancellationToken)
        {
            EntityEntry<PlayerStatistic> dbEntry = null;
            if (await _dbContext.PlayerStatistics.SingleOrDefaultAsync(statistic => statistic.CharacterId == request.CharacterId, cancellationToken) is { } playerStatistic)
            {
                playerStatistic.DamageDealt += request.DamageDealt;
                playerStatistic.DamageTaken += request.DamageTaken;
                dbEntry = _dbContext.PlayerStatistics.Update(playerStatistic);
            }
            else
            {
                dbEntry = await _dbContext.PlayerStatistics.AddAsync(_mapper.Map<PlayerStatistic>(request), cancellationToken);
            }

            _dbContext.SaveChanges();

            return _mapper.Map<ResponseWithMetaData<AddPlayerStatisticDto>>(dbEntry.Entity);
        }
    }
}