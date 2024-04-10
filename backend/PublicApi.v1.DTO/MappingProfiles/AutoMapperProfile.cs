using AutoMapper;
using PublicApi.v1.DTO.Search;

namespace PublicApi.v1.DTO.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Event, BLL.DTO.Event>().ReverseMap();
        CreateMap<Organization, BLL.DTO.Organization>().ReverseMap();
        CreateMap<Person, BLL.DTO.Person>().ReverseMap();
        CreateMap<PaymentMethod, BLL.DTO.PaymentMethod>().ReverseMap();

        CreateMap<EventSearch, DAL.DTO.Search.EventSearch>();
        CreateMap<OrganizationSearch, DAL.DTO.Search.OrganizationSearch>();
        CreateMap<PersonSearch, DAL.DTO.Search.PersonSearch>();
        CreateMap<PaymentMethodSearch, DAL.DTO.Search.PaymentMethodSearch>();
    }
}