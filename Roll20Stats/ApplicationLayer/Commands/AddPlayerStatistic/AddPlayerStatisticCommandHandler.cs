using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Roll20Stats.InfrastructureLayer.DAL.Models;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.Factories;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.SavingRepositories;

namespace Roll20Stats.ApplicationLayer.Commands.AddPlayerStatistic
{
    public class AddPlayerStatisticCommandHandler : IRequestHandler<AddPlayerStatisticCommand>
    {
        private readonly ISavingRepository<PlayerStatistic> _repository;
        private readonly IMapper _mapper;

        public AddPlayerStatisticCommandHandler(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repository = repositoryFactory.CreateSavingRepository<PlayerStatistic>();
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddPlayerStatisticCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.GetSingle(statistic => statistic.CharacterId == request.CharacterId) is { } playerStatistic)
            {
                playerStatistic.DamageDealt += request.DamageDealt;
                playerStatistic.DamageTaken += request.DamageTaken;
                _repository.Update(playerStatistic);
            }
            else
            {
                _repository.Add(_mapper.Map<PlayerStatistic>(request));
            }

            return Unit.Value;
        }
    }
}