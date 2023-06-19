using AutoMapper;
using Clean.Frame.Apps.TargetBoard.AutoMapper;
using Clean.Frame.Apps.TargetBoard.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Frame.Apps.TargetBoard.Configurations
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(AutoMapperConfig.RegisterMappings());
        }
    }
    
}
