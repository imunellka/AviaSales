using FluentMigrator;

namespace AviaSalesService.Repositories.Migrations;

[Migration(003, TransactionBehavior.None)]
public class AddFlightType : Migration {
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'flight_v1') THEN
            CREATE TYPE flight_v1 as
            (
                  id                int
                , departure         varchar
                , arrival           varchar
                , from_time         timestamp with time zone
                , to_time           timestamp with time zone
            );
        END IF;
    END
$$;";
        
        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS flight_v1;
    END
$$;";

        Execute.Sql(sql);
    }
}