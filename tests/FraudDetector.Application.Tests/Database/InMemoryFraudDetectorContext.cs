using FraudDetector.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace FraudDetector.Application.Tests.Database;

public static class InMemoryFraudDetectorContext
{
    public static FraudDetectorContext GetContext()
    {
        var builder = new DbContextOptionsBuilder<FraudDetectorContext>();
        builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        return new FraudDetectorContext(builder.Options);
    }
}
