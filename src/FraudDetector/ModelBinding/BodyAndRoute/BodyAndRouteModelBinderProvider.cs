using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace FraudDetector.ModelBinding.BodyAndRoute;

public class BodyAndRouteModelBinderProvider : IModelBinderProvider
{
    private readonly BodyModelBinderProvider _bodyModelBinderProvider;
    private readonly ComplexObjectModelBinderProvider _complexTypeModelBinderProvider;

    public BodyAndRouteModelBinderProvider(BodyModelBinderProvider bodyModelBinderProvider,
        ComplexObjectModelBinderProvider complexObjectModelBinderProvider)
    {
        _bodyModelBinderProvider = bodyModelBinderProvider;
        _complexTypeModelBinderProvider = complexObjectModelBinderProvider;
    }

    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var bodyBinder = _bodyModelBinderProvider.GetBinder(context);
        var complexBinder = _complexTypeModelBinderProvider.GetBinder(context);

        return context.BindingInfo.BindingSource != null
               && context.BindingInfo.BindingSource.CanAcceptDataFrom(BodyAndRouteBindingSource.BodyAndRoute)
               && bodyBinder != null
               && complexBinder != null
            ? new BodyAndRouteModelBinder(bodyBinder, complexBinder)
            : default(IModelBinder?);
    }
}