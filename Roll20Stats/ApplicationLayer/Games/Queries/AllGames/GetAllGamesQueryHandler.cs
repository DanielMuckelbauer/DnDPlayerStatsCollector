using AutoMapper;
using MediatR;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.PresentationLayer.DataTransferObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Roll20Stats.ApplicationLayer.Games.Queries.SingleGame
{
    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<GameDto>>
    {
        private readonly IApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllGamesQueryHandler(IApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<IEnumerable<GameDto>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_mapper.Map<IEnumerable<GameDto>>(_dbContext.Games));
        }
    }
}
