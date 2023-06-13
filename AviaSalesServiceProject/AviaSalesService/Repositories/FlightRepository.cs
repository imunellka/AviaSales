using AviaSalesService.Models;
using Dapper;

namespace AviaSalesService.Repositories;

public class FlightRepository : BaseRepository
{
    public async Task<int[]> Add(
        Flight[] flights)
    {
        const string sqlQuery = @"
insert into flight (departure, arrival, from_time, to_time)
select departure, arrival, from_time, to_time
  from UNNEST(@Flights)
returning id;
";
        
        var sqlQueryParams = new
        {
            Flights = flights
        };
        
        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return result.ToArray();
    }
    
    public async Task<Flight?> GetFlightById(
        int id)
    {
        const string sqlQuery = @"
select id
     , departure
     , arrival
     , from_time
     , to_time
from flight
where id = @Id
";
        
        var sqlQueryParams = new
        {
            Id = id
        };
        
        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<Flight>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return result.FirstOrDefault();
    }
    
    public async Task<Flight[]> GetAllFlights()
    {
        string sqlQuery = @"
select id
     , departure
     , arrival
     , from_time
     , to_time
from flight
order by id
";

        await using var connection = await GetAndOpenConnection();
        var flights = await connection.QueryAsync<Flight>(
            new CommandDefinition(
                sqlQuery));
        
        return flights
            .ToArray();
    }

}