using AutoMapper;
using Roll20Stats.ApplicationLayer.Queries.PlayerStatistics;
using Roll20Stats.InfrastructureLayer.DAL.Models;

namespace Roll20Stats.InfrastructureLayer.AutoMapperProfiles
{
    public class PlayerStatisticProfile : Profile
    {
        public PlayerStatisticProfile()
        {
            CreateMap<PlayerStatistic, PlayerStatisticDTO>();
        }
    }
}
