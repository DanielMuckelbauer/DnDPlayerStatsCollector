using AutoMapper;
using Roll20Stats.ApplicationLayer.Commands.AddPlayerStatistic;
using Roll20Stats.ApplicationLayer.DataTransferObjects;
using Roll20Stats.InfrastructureLayer.DAL.Models;

namespace Roll20Stats.InfrastructureLayer.AutoMapperProfiles
{
    public class PlayerStatisticProfile : Profile
    {
        public PlayerStatisticProfile()
        {
            CreateMap<PlayerStatistic, PlayerStatisticDTO>();
            CreateMap<AddPlayerStatisticCommand, PlayerStatistic>();
        }
    }
}
