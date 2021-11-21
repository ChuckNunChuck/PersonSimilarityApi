using FraudDetector.Application.Models;

namespace FraudDetector.Application.Extensions;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize) => 
        PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
}