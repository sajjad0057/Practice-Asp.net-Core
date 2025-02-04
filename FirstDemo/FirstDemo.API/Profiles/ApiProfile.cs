using AutoMapper;
using FirstDemo.API.Models;
using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;

namespace FirstDemo.API.Profiles;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<CourseModel, CourseBO>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Title))
            .ReverseMap();
    }
}
