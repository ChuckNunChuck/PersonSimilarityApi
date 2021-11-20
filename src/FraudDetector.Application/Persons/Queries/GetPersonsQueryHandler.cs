using AutoMapper;
using AutoMapper.QueryableExtensions;
using FraudDetector.Application.Common.Models;
using FraudDetector.Application.Extensions;
using FraudDetector.Infrastructure.Database;
using MediatR;

namespace FraudDetector.Application.Persons.Queries;

public class GetPersonsQueryHandler 
    : IRequestHandler<GetPersonsQuery, PaginatedList<PersonDto>>
{
    private readonly FraudDetectorContext _context;
    private readonly IMapper _mapper;

    public GetPersonsQueryHandler(FraudDetectorContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PersonDto>> Handle(
        GetPersonsQuery request, 
        CancellationToken cancellationToken) =>
        await _context.Persons
            .OrderBy(x => x.LastName)
            .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
}