using FluentValidation;
using FluentValidation.AspNetCore;
using FraudDetector.Application;
using FraudDetector.Application.Persons.Commands.CreatePersonCommand;
using FraudDetector.Application.Settings;
using FraudDetector.ConfigurationValidation;
using FraudDetector.ConfigurationValidation.Validators;
using FraudDetector.Extensions;
using FraudDetector.Infrastructure.Extensions;
using FraudDetector.ModelBinding;

namespace FraudDetector;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        _configuration.ThrowOnInvalidConfiguration(
            new List<AbstractValidator<IConfiguration>>
            {
                new PersonSimilarityWeightsValidator()
            });

        services
            .AddControllers(options =>
            {
                options.ModelBinderProviders.InsertBodyAndRouteBinding();
            })
            .AddFluentValidation(fv
                => fv.RegisterValidatorsFromAssemblyContaining<CreatePersonCommandValidator>());

        services
            .AddSettings<PersonSimilarityWeights>(_configuration)
            .AddInMemoryFraudDetectorStore()
            .AddResources()
            .AddApplication()
            .AddLocalAuthorization()
            .AddSwagger()
            .AddHealthChecks();
    }

    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHealthChecks("/healthz")
            .UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            })
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}