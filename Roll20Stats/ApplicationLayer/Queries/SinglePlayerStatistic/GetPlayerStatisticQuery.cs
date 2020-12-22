﻿using MediatR;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Queries.SinglePlayerStatistic
{
    public class GetPlayerStatisticQuery : IRequest<ResponseWithMetaData<GetPlayerStatisticDto>>
    {
        public string CharacterId { get; set; }
        public string GameName { get; set; }
    }
}
