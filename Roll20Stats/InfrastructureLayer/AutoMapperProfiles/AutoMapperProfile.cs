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
            CreateMap<PlayerStatistic, AddPlayerStatisticDto>();


            CreateMap<PlayerStatistic, ResponseWrapper<GetPlayerStatisticDto>>()
                .ForMember(dest => dest.Response,
                    opt => opt.MapFrom(src => src));
            CreateMap<PlayerStatistic, ResponseWrapper<AddPlayerStatisticDto>>()
                .ForMember(dest => dest.Response,
                    opt => opt.MapFrom(src => src));

            CreateMap<Game, CreateGameDto>();
            CreateMap<CreateGameCommand, Game>();
            CreateMap<Game, ResponseWrapper<CreateGameDto>>()
                .ForMember(dest => dest.Response,
                    opt => opt.MapFrom(src => src));
        }
    }
}
