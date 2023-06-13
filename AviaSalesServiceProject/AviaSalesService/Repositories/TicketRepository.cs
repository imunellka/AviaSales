using AviaSalesService.Models;
using Dapper;

namespace AviaSalesService.Repositories;

public class TicketRepository : BaseRepository
{
    public async Task<int> Add(
        int flightId,
        int passengerId)
    {
        const string sqlQuery = @"
insert into ticket (flight_id, passenger_id, active) values (@FlightId, @PassengerId, @Active)
returning id;
";
        
        var sqlQueryParams = new
        {
            FlightId = flightId,
            PassengerId = passengerId,
            Active = true
        };
        
        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return result.First();
    }
    
    public async Task<Ticket[]> GetAllPassengerTickets(int passengerId)
    {
        string sqlQuery = @"
select id, flight_id, passenger_id, active
from ticket
where passenger_id = @PassengerId
order by id
";

        var sqlQueryParams = new
        {
            PassengerId = passengerId
        };
        
        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<Ticket>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
        
        return result.ToArray();
    }
    
    public async Task DeleteTicket(int ticketId)
    {
        string sqlQuery = @"
update ticket
set active = false
where id = @TicketId
";

        var sqlQueryParams = new
        {
            TicketId = ticketId
        };
        
        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<Ticket>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams));
    }
}