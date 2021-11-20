using FraudDetector.ModelBinding.BodyAndRoute;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace FraudDetector.ModelBinding;

public static class ModelBinderProviderExtensions
{
    public static IList<IModelBinderProvider> InsertBodyAndRouteBinding(this IList<IModelBinderProvider> providers)
    {
        var bodyProvider = providers.OfType<BodyModelBinderProvider>()
            .Single();
        var complexProvider = providers.OfType<ComplexObjectModelBinderProvider>()
            .Single();

        var bodyAndRouteProvider = new BodyAndRouteModelBinderProvider(bodyProvider, complexProvider);

        providers.Insert(0, bodyAndRouteProvider);
        return providers;
    }
}