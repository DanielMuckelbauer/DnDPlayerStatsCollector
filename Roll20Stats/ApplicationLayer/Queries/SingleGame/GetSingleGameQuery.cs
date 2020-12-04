using MediatR;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Queries.SingleGame
{
    public class GetSingleGameQuery : IRequest<ResponseWithMetaData<GameDto>>
    {
        public string Name { get; set; }
    }
}
