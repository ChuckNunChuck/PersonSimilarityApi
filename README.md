# Fraud Detector 
[![.NET](https://github.com/divanchyshyn/FraudDetector/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/divanchyshyn/FraudDetector/actions/workflows/dotnet.yml)

.NET 6 based service for probability calculations that 2 persons are the same physical person

## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [xUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)

## Getting Started

1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Navigate to `src/FraudDetector` and run `dotnet run` to launch the API
3. Check `https://localhost:5001/swagger/index.html` for API documentation
4. For authorization use locally generated JWT token from the Swagger authorization dialog
5. Git Demo

![FraudDetectionApi](https://user-images.githubusercontent.com/9357531/143013332-12559473-1af7-4227-8050-5818c862cc4e.png)