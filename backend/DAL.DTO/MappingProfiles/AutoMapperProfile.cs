using AutoMapper;

namespace DAL.DTO.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Event, Domain.App.Event>().ReverseMap();
        CreateMap<Organization, Domain.App.Organization>().ReverseMap();
        CreateMap<Person, Domain.App.Person>().ReverseMap();
        CreateMap<PaymentMethod, Domain.App.PaymentMethod>().ReverseMap();
    }
}