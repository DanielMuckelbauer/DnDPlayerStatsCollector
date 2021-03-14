using MediatR;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Games.Queries.SingleGame
{
    public class GetSingleGameQuery : IRequest<ResponseWithMetaData<GameDto>>
    {
        public string Name { get; set; }

        public GetSingleGameQuery(string name)
        {
            Name = name;
        }
    }
}
