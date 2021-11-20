using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FraudDetector.ModelBinding.BodyAndRoute;

public class BodyAndRouteBindingSource : BindingSource
{
    public static readonly BindingSource BodyAndRoute = new BodyAndRouteBindingSource(
        "BodyAndRoute",
        "BodyAndRoute",
        true,
        true
    );

    public BodyAndRouteBindingSource(string id, string displayName, bool isGreedy, bool isFromRequest) : base(id,
        displayName, isGreedy, isFromRequest)
    {
    }

    public override bool CanAcceptDataFrom(BindingSource bindingSource) =>
        bindingSource == Body || bindingSource == this;
}