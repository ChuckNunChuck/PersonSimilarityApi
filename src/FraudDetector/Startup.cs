using FluentValidation.AspNetCore;
using FraudDetector.Application;
using FraudDetector.Application.Persons.Commands.CreatePersonCommand;
using FraudDetector.Extensions;
using FraudDetector.Infrastructure.Extensions;
using FraudDetector.ModelBinding;

namespace FraudDetector;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers(options =>
            {
                options.ModelBinderProviders.InsertBodyAndRouteBinding();
            })
            .AddFluentValidation(fv
                => fv.RegisterValidatorsFromAssemblyContaining<CreatePersonCommandValidator>());

        services
            .AddInMemoryFraudDetectorStore()
            .AddApplication()
            .AddSwagger()
            .AddHttpContextAccessor()
            .AddHealthChecks();
    }

    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        ILogger<Startup> logger)
    {
        var pathBase = Configuration["PATH_BASE"];
        if (!string.IsNullOrEmpty(pathBase))
        {
            logger.LogInformation("Using PATH BASE '{pathBase}'", pathBase);
            app.UsePathBase(pathBase);
        }

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHealthChecks("/health");
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });

        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}