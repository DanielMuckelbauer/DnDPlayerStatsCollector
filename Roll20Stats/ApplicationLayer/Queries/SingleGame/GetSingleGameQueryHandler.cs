using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Queries.SingleGame
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


        public async Task<ResponseWithMetaData<GameDto>> Handle(GetSingleGameQuery request, CancellationToken cancellationToken)
        {
            var game = await _dbContext.Games.SingleOrDefaultAsync(game => game.Name == request.Name, cancellationToken);
            return game is { }
                ? _mapper.Map<ResponseWithMetaData<GameDto>>(game)
                : new ResponseWithMetaData<GameDto>
                {
                    HasError = true,
                    StatusCode = 404,
                    ErrorMessage = $@"Game with name ""{request.Name}"" does not exist."
                };
        }
    }
}
