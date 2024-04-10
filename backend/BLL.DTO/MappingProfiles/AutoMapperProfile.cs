using AutoMapper;

namespace BLL.DTO.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Event, DAL.DTO.Event>().ReverseMap();
        CreateMap<Organization, DAL.DTO.Organization>().ReverseMap();
        CreateMap<Person, DAL.DTO.Person>().ReverseMap();
        CreateMap<PaymentMethod, DAL.DTO.PaymentMethod>().ReverseMap();
    }
}