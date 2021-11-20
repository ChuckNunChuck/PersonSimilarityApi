using FraudDetector.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FraudDetector.Infrastructure.Extensions;

public static class InfrastructureExtensions
{

    public static IServiceCollection AddInMemoryFraudDetectorStore(
        this IServiceCollection services) =>
        services.AddDbContext<FraudDetectorContext>(options => 
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
}
