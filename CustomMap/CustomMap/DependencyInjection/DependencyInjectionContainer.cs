using CustomMap.Services;
using CustomMap.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMap.DependencyInjection
{
    public static class DependencyInjectionContainer
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<ILocationService, LocationService>();
            
            return services;
        }
        
        public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
        {
            services.AddTransient<LocationListViewModel>();
            services.AddTransient<MapViewModel>();
            
            return services;
        }
    }
}