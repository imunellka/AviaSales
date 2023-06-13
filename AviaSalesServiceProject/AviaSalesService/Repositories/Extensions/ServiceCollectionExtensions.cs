namespace AviaSalesService.Repositories.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<FlightRepository>();
        services.AddScoped<TicketRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        Postgres.MapCompositeTypes();
        
        Postgres.AddMigrations(services);

        return services;
    }
}