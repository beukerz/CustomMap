using System;
using CustomMap.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMap
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static IServiceProvider Init()
        {
            var serviceProvider = new ServiceCollection()
                .ConfigureServices()
                .ConfigureViewModels()
                .BuildServiceProvider();
            
            ServiceProvider = serviceProvider;
            
            return serviceProvider;
        }
    }
}