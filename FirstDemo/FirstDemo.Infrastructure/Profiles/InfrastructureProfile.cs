using AutoMapper;
using BO = FirstDemo.Infrastructure.BusinessObjects;
using EO = FirstDemo.Infrastructure.Entities;


namespace FirstDemo.Infrastructure.Profiles
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {

            CreateMap<EO.Course, BO.Course>()
                .ForMember(dest => dest.Name, Src => Src.MapFrom(x => x.Title))
                .ReverseMap();


            CreateMap<EO.Student, BO.Student>()
                .ReverseMap();


            CreateMap<EO.Topic, BO.Topic>()
                .ReverseMap();

        }
    }
}
