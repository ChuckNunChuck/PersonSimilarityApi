using FraudDetector.Infrastructure.Database;
using MediatR;

namespace FraudDetector.Application.Persons.Queries;

public class GetSimilarityQueryHandler
    : IRequestHandler<GetSimilarityQuery, decimal>
{
    private readonly FraudDetectorContext _context;

    public GetSimilarityQueryHandler(FraudDetectorContext context)
    {
        _context = context;
    }

    public Task<decimal> Handle(
        GetSimilarityQuery request, 
        CancellationToken cancellationToken) =>
       Task.FromResult(0m);
}