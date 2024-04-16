using Microsoft.Extensions.DependencyInjection;

namespace Common.Repository.SqlServer
{
    public static class Configuration
    {
        public static IServiceCollection AddSqlRepository<T>(this IServiceCollection services)
            where T : Entity =>
            services.AddScoped<ICommonRepository<T>, SqlRepository<T>>();

        public static IServiceCollection AddSqlUnitOfWork<T>(this IServiceCollection services)
            where T : Entity =>
            services.AddScoped<ISqlUnitOfWork, SqlUnitOfWork>();
    }
}