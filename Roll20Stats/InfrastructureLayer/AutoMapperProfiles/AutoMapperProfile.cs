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
            CreateMap<PlayerStatistic, GetPlayerStatisticRequest>();
            CreateMap<AddPlayerStatisticCommand, PlayerStatistic>();
            CreateMap<PlayerStatistic, AddPlayerStatisticRequest>();


            CreateMap<PlayerStatistic, ResponseWithMetaData<GetPlayerStatisticRequest>>()
                .ForMember(dest => dest.Response,
                    opt => opt.MapFrom(src => src));
            CreateMap<PlayerStatistic, ResponseWithMetaData<AddPlayerStatisticRequest>>()
                .ForMember(dest => dest.Response,
                    opt => opt.MapFrom(src => src));

            CreateMap<Game, GameDto>();
            CreateMap<CreateGameCommand, Game>();
            CreateMap<Game, ResponseWithMetaData<GameDto>>()
                .ForMember(dest => dest.Response,
                    opt => opt.MapFrom(src => src));
        }
    }
}
