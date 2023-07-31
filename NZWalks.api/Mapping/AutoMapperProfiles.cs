using AutoMapper;
using NZWalks.api.Models.Domain;
using NZWalks.api.Models.DTO;

namespace NZWalks.api.Mapping
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Region,UpdateRegionDTO>().ReverseMap();
            CreateMap<Region,DeleteRegionDTO>().ReverseMap();
            CreateMap<Walk, AddWalkDTO>().ReverseMap();
            CreateMap<Difficulty,DifficultyDTO>().ReverseMap();
            CreateMap<Region,AddRegionDTO>().ReverseMap();
            CreateMap<Walk,UpdateWalkDTO>().ReverseMap();
        }
    }
}
