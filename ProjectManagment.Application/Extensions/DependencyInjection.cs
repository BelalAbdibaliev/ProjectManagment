using Microsoft.Extensions.DependencyInjection;
using ProjectManagment.Application.Interfaces.Services;
using ProjectManagment.Application.Services;

namespace ProjectManagment.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddAutoMapper(typeof(MappingProfile));
        
        return services;
    }
}