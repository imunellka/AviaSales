using AviaSalesService.Models;
using FluentMigrator.Exceptions;
using FluentMigrator.Runner;
using Npgsql;
using Npgsql.NameTranslation;


namespace AviaSalesService.Repositories.Extensions;

public static class Postgres
{
    private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

    public static void MapCompositeTypes()
    {
        var mapper = NpgsqlConnection.GlobalTypeMapper;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        mapper.MapComposite<Flight>("flight_v1", Translator);
        mapper.MapComposite<Ticket>("ticket_v1", Translator);
    }

    public static void AddMigrations(IServiceCollection services)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(s =>
                    "User ID=postgres;Password=123456;Host=localhost;Port=15432;Database=avia_sales;Pooling=true;")
                .ScanIn(typeof(Postgres).Assembly).For.Migrations()
            )
            .AddLogging(lb => lb.AddFluentMigratorConsole());
    }
}