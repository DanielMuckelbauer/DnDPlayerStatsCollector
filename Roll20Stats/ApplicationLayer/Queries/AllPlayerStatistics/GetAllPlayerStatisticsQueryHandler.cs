using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Roll20Stats.ApplicationLayer.DataTransferObjects;
using Roll20Stats.InfrastructureLayer.DAL.Models;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.Factories;
using Roll20Stats.InfrastructureLayer.DAL.Repositories.ReadOnlyRepository;

namespace Roll20Stats.ApplicationLayer.Queries.AllPlayerStatistics
{
    public class GetAllPlayerStatisticsQueryHandler : IRequestHandler<GetAllPlayerStatisticsQuery, IEnumerable<PlayerStatisticDTO>>
    {
        private readonly IReadOnlyRepository<PlayerStatistic> _repository;
        private readonly IMapper _mapper;

        public GetAllPlayerStatisticsQueryHandler(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repositoryFactory.CreateReadOnlyRepository<PlayerStatistic>();
        }

        public Task<IEnumerable<PlayerStatisticDTO>> Handle(GetAllPlayerStatisticsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_mapper.Map<IEnumerable<PlayerStatisticDTO>>(_repository.QueryAll()));
        }
    }
}
