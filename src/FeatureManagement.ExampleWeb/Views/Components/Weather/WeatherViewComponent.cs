namespace FeatureManagement.ExampleWeb.Views.Components.Weather
{
    public class WeatherViewComponent : ViewComponent
    {
        private readonly IFeatureManager _featureManager;
        private readonly Random _random = new Random();

        public WeatherViewComponent(IFeatureManager featureManager)
            => _featureManager = featureManager;

        public async Task<IViewComponentResult> InvokeAsync()
            => await _featureManager.IsEnabledAsync("Weather")
                ? View(new WeatherViewModel(_random.Next(-30, 30)))
                : Content(string.Empty);
    }
}