using FeatureManagement.ExampleWeb;
using FeaturesManagementDashboard.Extensions;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(configure => configure.ClearProviders())
    .ConfigureWebHostDefaults(webHost => webHost.UseStartup<Startup>());

using var app = builder.Build();

await app.RunAsync();

namespace FeatureManagement.ExampleWeb
{
#pragma warning disable SA1649 // File name should match first type name

    internal class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddUmbraco(_env, _config)
                .AddBackOffice()
                .AddWebsite()
                .AddComposers()
                .AddFeaturesManagementDashboard()
                .Build();

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseUmbraco()
                .WithMiddleware(u =>
                {
                    u.UseBackOffice();
                    u.UseWebsite();
                })
                .WithEndpoints(u =>
                {
                    u.UseInstallerEndpoints();
                    u.UseBackOfficeEndpoints();
                    u.UseWebsiteEndpoints();
                });
        }
    }

#pragma warning restore SA1649 // File name should match first type name
}