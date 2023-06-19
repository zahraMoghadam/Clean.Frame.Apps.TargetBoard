using Clean.Frame.Apps.TargetBoard.Core.Repositories;
using Clean.Frame.Apps.TargetBoard.Core.Services;
using Clean.Frame.Apps.TargetBoard.Core.UnitOfWorks;
using Clean.Frame.Apps.TargetBoard.Data.Repositories;
using Clean.Frame.Apps.TargetBoard.Data.UnitOfWorks;
using Clean.Frame.Apps.TargetBoard.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Frame.Apps.TargetBoard.Services.Services;
using Clean.Frame.Apps.TargetBoard.Services.Services.Units;
using IUnitService = Clean.Frame.Apps.TargetBoard.Services.Services.IUnitService;

namespace Clean.Frame.Apps.TargetBoard.Services.NativeInjectorBootStrapper
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<ITargetService, TargetService>();
            services.AddScoped<IMainBoardService, MainBoardService>();
            services.AddScoped<IAspectService, AspectService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
