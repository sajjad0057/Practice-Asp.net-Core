using AutoMapper;
using FirstDemo.Web.Areas.Admin.Models;
using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;


namespace FirstDemo.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CourseCreateModel, CourseBO>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Title))
                .ReverseMap();


            CreateMap<CourseEditModel, CourseBO>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Title))
                .ReverseMap();
        }
    }
}
