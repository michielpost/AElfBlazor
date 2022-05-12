using Microsoft.Extensions.DependencyInjection;

namespace AElfBlazor
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAElfBlazor(this IServiceCollection services)
        {
            services.AddScoped<AElfService>();
        }
    }
}
