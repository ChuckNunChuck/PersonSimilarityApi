using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FraudDetector.ModelBinding.BodyAndRoute;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
public class FromBodyAndRouteAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource BindingSource => BodyAndRouteBindingSource.BodyAndRoute;
}