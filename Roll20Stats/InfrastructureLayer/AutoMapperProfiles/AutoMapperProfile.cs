using AutoMapper;
using Roll20Stats.ApplicationLayer.Games.Commands.CreateGame;
using Roll20Stats.ApplicationLayer.PlayerStatistics.Commands.AddPlayerStatistic;
using Roll20Stats.InfrastructureLayer.DAL.Entities;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.InfrastructureLayer.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PlayerStatistic, PlayerStatisticDto>();
            CreateMap<AddPlayerStatisticCommand, PlayerStatistic>();
            CreateMap<PlayerStatistic, PlayerStatisticDto>();


            CreateMap<PlayerStatistic, ResponseWithMetaData<PlayerStatisticDto>>()
                .ForMember(dest => dest.Response,
                    opt => opt.MapFrom(src => src))
            .ForPath(dest => dest.Response.GameName,
                    opt => opt.MapFrom(src => src.Game.Name));

            CreateMap<PlayerStatistic, ResponseWithMetaData<PlayerStatisticDto>>()
                .ForMember(dest => dest.Response,
                    opt => opt.MapFrom(src => src))
                .ForPath(dest => dest.Response.GameName,
                    opt => opt.MapFrom(src => src.Game.Name));

            CreateMap<Game, GameDto>();
            CreateMap<CreateGameCommand, Game>();
            CreateMap<Game, ResponseWithMetaData<GameDto>>()
                .ForMember(dest => dest.Response,
                    opt => opt.MapFrom(src => src));
        }
    }
}
