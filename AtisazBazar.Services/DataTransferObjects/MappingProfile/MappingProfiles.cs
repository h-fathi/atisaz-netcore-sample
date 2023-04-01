using AtisazBazar.DataAccess;
using AutoMapper;

namespace AtisazBazar.Services.DataTransferObjects.MappingProfile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Todo, TodoVM>().ReverseMap();
        }
    }
}
