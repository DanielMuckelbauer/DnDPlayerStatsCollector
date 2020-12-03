using AutoMapper;
using Roll20Stats.ApplicationLayer.Commands.AddPlayerStatistic;
using Roll20Stats.ApplicationLayer.Commands.CreateGame;
using Roll20Stats.InfrastructureLayer.DAL.Entities;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.InfrastructureLayer.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PlayerStatistic, GetPlayerStatisticDto>();
            CreateMap<AddPlayerStatisticCommand, PlayerStatistic>();

            CreateMap<Game, CreateGameDto>();
            CreateMap<CreateGameCommand, Game>();
        }
    }
}
