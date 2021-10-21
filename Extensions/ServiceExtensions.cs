using matxicorp.Data.Contracts;
using matxicorp.Data.Repository;
using Microsoft.Extensions.DependencyInjection;


namespace matxicorp.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
