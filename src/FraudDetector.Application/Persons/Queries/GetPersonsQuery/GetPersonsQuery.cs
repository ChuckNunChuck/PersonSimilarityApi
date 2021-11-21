using FraudDetector.Application.Model;
using MediatR;

namespace FraudDetector.Application.Persons.Queries.GetPersonsQuery;

public class GetPersonsQuery : IRequest<PaginatedList<PersonDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}