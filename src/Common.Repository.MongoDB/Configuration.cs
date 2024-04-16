using Microsoft.Extensions.DependencyInjection;

namespace Common.Repository.MongoDB
{
    public static class Configuration
    {
        public static IServiceCollection AddSqlRepository<T>(this IServiceCollection services)
         where T : Entity =>
         services.AddScoped<ICommonRepository<T>, MongoDBRepository<T>>();

        public static IServiceCollection AddSqlUnitOfWork<T>(this IServiceCollection services)
            where T : Entity =>
            services.AddScoped<IMongoUnitOfWork, MongoDBUnitOfWork>();
    }
}