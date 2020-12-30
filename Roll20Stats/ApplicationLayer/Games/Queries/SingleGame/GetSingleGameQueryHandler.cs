using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Games.Queries.SingleGame
{
    public class GetSingleGameQueryHandler : IRequestHandler<GetSingleGameQuery, ResponseWithMetaData<GameDto>>
    {
        private readonly IApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public GetSingleGameQueryHandler(IApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<ResponseWithMetaData<GameDto>> Handle(GetSingleGameQuery request, CancellationToken _)
        {
            var game = await _dbContext.Games.SingleOrDefaultAsync(game => game.Name == request.Name);
            return game is { }
                ? _mapper.Map<ResponseWithMetaData<GameDto>>(game)
                : new ResponseWithMetaData<GameDto>(true, 404, $@"Game with name ""{request.Name}"" does not exist.");
        }
    }
}
