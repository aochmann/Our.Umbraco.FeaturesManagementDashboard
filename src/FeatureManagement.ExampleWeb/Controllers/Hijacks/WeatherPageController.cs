using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace FeatureManagement.ExampleWeb.Controllers.Hijacks
{
    [FeatureGate("WeatherPage")]
    public class WeatherPageController : RenderController
    {
        public WeatherPageController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }
    }
}