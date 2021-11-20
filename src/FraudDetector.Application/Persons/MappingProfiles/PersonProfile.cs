using AutoMapper;
using FraudDetector.Application.Persons.Queries;
using FraudDetector.Domain.Model;

namespace FraudDetector.Application.Persons.MappingProfiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonDto>();
    }
}
