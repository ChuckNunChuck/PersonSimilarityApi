using FraudDetector.Infrastructure.Database;
using FraudDetector.Infrastructure.Interfaces;
using FraudDetector.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FraudDetector.Infrastructure.Extensions;

public static class InfrastructureExtensions
{

    public static IServiceCollection AddInMemoryFraudDetectorStore(
        this IServiceCollection services)
    {
        var databaseName  = Guid.NewGuid().ToString();
        return services.AddDbContext<FraudDetectorContext>(options =>
            options.UseInMemoryDatabase(databaseName));
    }

    public static IServiceCollection AddResources(
        this IServiceCollection services) =>
        services
            .AddScoped<IResourceReader, ResourceReader>()
            .AddSingleton<IDiminutiveNameService, DiminutiveNameService>();
}
