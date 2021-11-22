namespace FraudDetector.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddSettings<T>(
        this IServiceCollection services, IConfiguration configuration)
    where  T : class
    {
        var settings = Activator.CreateInstance<T>();
        configuration.GetSection(typeof(T).Name).Bind(settings);
        
        return services.AddSingleton(settings);
    }
}