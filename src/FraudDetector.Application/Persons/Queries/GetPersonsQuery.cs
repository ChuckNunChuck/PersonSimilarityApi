using FraudDetector.Application.Common.Models;
using MediatR;

namespace FraudDetector.Application.Persons.Queries;

public class GetPersonsQuery : IRequest<PaginatedList<PersonDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}