using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FeatureManagement.ExampleWeb
{
    public static class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args)
                .Build()
                .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
#if NET7_0_OR_GREATER || NET6_0_OR_GREATER
                .ConfigureUmbracoDefaults()
#endif
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStaticWebAssets();
                    webBuilder.UseStartup<Startup>();
                });
    }
}