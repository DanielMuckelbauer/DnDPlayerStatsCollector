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
    public class AddPlayerStatisticCommandHandler : IRequestHandler<AddPlayerStatisticCommand, ResponseWithMetaData<AddPlayerStatisticRequest>>
    {
        private readonly IApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public AddPlayerStatisticCommandHandler(IApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResponseWithMetaData<AddPlayerStatisticRequest>> Handle(AddPlayerStatisticCommand request, CancellationToken cancellationToken)
        {
            return await GetExistingPlayerStatistic(request, cancellationToken) is { } playerStatistic
                ? UpdatePlayerStatistic(request, playerStatistic)
                : await CreatePlayerStatistic(request, cancellationToken);
        }

        private async Task<ResponseWithMetaData<AddPlayerStatisticRequest>> CreatePlayerStatistic(AddPlayerStatisticCommand request, CancellationToken cancellationToken)
        {
            var dbEntry = await _dbContext.PlayerStatistics.AddAsync(_mapper.Map<PlayerStatistic>(request), cancellationToken);
            _dbContext.SaveChanges();
            return _mapper.Map<ResponseWithMetaData<AddPlayerStatisticRequest>>(dbEntry.Entity);
        }

        private ResponseWithMetaData<AddPlayerStatisticRequest> UpdatePlayerStatistic(AddPlayerStatisticCommand request, PlayerStatistic playerStatistic)
        {
            playerStatistic.DamageDealt += request.DamageDealt;
            playerStatistic.DamageTaken += request.DamageTaken;
            var dbEntry = _dbContext.PlayerStatistics.Update(playerStatistic);
            _dbContext.SaveChanges();
            return _mapper.Map<ResponseWithMetaData<AddPlayerStatisticRequest>>(dbEntry.Entity);
        }

        private Task<PlayerStatistic> GetExistingPlayerStatistic(AddPlayerStatisticCommand request, CancellationToken cancellationToken)
        {
            return _dbContext.PlayerStatistics.SingleOrDefaultAsync(statistic => statistic.CharacterId == request.CharacterId, cancellationToken);
        }
    }
}