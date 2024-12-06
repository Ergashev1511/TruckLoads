using MediatR;
using System.Reflection;
namespace TruckLoads.DependencyExtensions
{
    public static class MediatRHandler
    {
        public static IServiceCollection AddMediatRHandlers(this IServiceCollection services)
        {
            // MediatR handlerlarini ro'yxatdan o'tkazish
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(TruckLoads.Services.MediatR.Commands.UserCommands.UserCreateCommand))));

            return services;
        }
    }
}
