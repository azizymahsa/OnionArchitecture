using System.Security.Claims;
using System.Security.Principal;
using Domain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Service
{
    public static class DependencyRegistrar
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPrincipal>(provider => provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.User ?? ClaimsPrincipal.Current);
            services.AddSingleton<IEPPlusService, EPPlusService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserPermissionService, UserPermissionService>();
        }
    }
}
