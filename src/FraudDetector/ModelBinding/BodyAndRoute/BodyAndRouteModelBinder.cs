using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FraudDetector.ModelBinding.BodyAndRoute;

public class BodyAndRouteModelBinder : IModelBinder
{
    private readonly IModelBinder _bodyBinder;
    private readonly IModelBinder _complexBinder;

    public BodyAndRouteModelBinder(IModelBinder bodyBinder, IModelBinder complexBinder)
    {
        _bodyBinder = bodyBinder;
        _complexBinder = complexBinder;
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        await _bodyBinder.BindModelAsync(bindingContext);

        if (bindingContext.Result.IsModelSet) bindingContext.Model = bindingContext.Result.Model!;
        else bindingContext.ModelState.Remove(string.Empty);

        await _complexBinder.BindModelAsync(bindingContext);
    }
}