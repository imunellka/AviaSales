using FluentMigrator;

namespace AviaSalesService.Repositories.Migrations;

[Migration(002, TransactionBehavior.None)]
public class InitShema : Migration {
    public override void Up()
    {
        Create.Table("flight")
            .WithColumn("id").AsInt32().PrimaryKey("flight_pk").Identity()
            .WithColumn("departure").AsString().NotNullable()
            .WithColumn("arrival").AsString().NotNullable()
            .WithColumn("from_time").AsDateTimeOffset().NotNullable()
            .WithColumn("to_time").AsDateTimeOffset().NotNullable();

        Create.Table("ticket")
            .WithColumn("id").AsInt32().PrimaryKey("id_pk").Identity()
            .WithColumn("flight_id").AsInt32().NotNullable()
            .WithColumn("passenger_id").AsInt32().NotNullable()
            .WithColumn("active").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("user");
        Delete.Table("session");
    }
}