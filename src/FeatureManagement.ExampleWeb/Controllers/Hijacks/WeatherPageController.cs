namespace FeatureManagement.ExampleWeb.Controllers.Hijacks;

[FeatureGate("WeatherPage")]
public class WeatherPageController : RenderController
{
    public WeatherPageController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }
}