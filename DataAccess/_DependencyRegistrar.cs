using DataAccess.Configuration.Register;
using Domain.Dto._Base;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.DataContext;
using Domain.DataAccess.DataContext;
using DataAccess.Repository;
using Domain.DataAccess.Repository;

namespace DataAccess
{
    public static class DependencyRegistrar
    {
        public static void RegisterDataAccess(this IServiceCollection services)
        {
            EntityConfig.SetEntitiesConfigurations();

            services.AddSingleton<IConnectionKeeper, ConnectionKeeper>();
            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserPermissionRepository, UserPermissionRepository>();
        }

        public static ApplicationSettingsDto GetApplicationSettings(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var applicationSettingsOptions = provider.GetRequiredService<IOptionsSnapshot<ApplicationSettingsDto>>();
            var applicationSettings = applicationSettingsOptions.Value;
            if (applicationSettings == null) throw new ArgumentNullException(nameof(applicationSettings));
            return applicationSettings;
        }
    }
}
