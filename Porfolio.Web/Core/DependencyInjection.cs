using Microsoft.EntityFrameworkCore;
using Porfolio.Web.Integrations.Github;
using Porfolio.Web.Repository;
using Porfolio.Web.Services.Email;
using Porfolio.Web.Services.MemoryCache;

namespace Porfolio.Web.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddHttpClient<IGithubService, GithubService>();
        services.Configure<GithubOptions>(builder.Configuration.GetSection("GitHub"));
        
        services.Configure<EmailOptions>(builder.Configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();
        
        services.AddSingleton<IApiCacheService, ApiMemoryCacheService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAboutRepository, AboutRepository>();

        return services;
    }

    public static IServiceCollection AddPorfolioDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PortfolioContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
