using BookApi.Repositories;
using BookApi.Repositories.Interfaces;
using BookApi.Services;
using BookApi.Services.Interfaces;

namespace BookApi.Middleware;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // In-memory store should be Singleton so the list persists while API runs
        services.AddSingleton<IBookRepository, InMemoryBookRepository>();

        // Service per request is a good default
        services.AddScoped<IBookService, BookService>();

        return services;
    }
}
