using AviaSalesService.Models;
using AviaSalesService.Repositories;

namespace AviaSalesService.Services;

public class AviaSalesHelperService
{
    private readonly FlightRepository _flightRepository;
    private readonly TicketRepository _ticketRepository;
    
    public AviaSalesHelperService(
        FlightRepository flightRepository,
        TicketRepository ticketRepository)
    {
        _flightRepository = flightRepository;
        _ticketRepository = ticketRepository;
    }

    public async Task<int[]> AddFlights(Flight[] flights)
    {
        var flightsIds = await _flightRepository.Add(flights);
        return flightsIds;
    }

    public async Task<Flight[]> GetAllFlights()
    {
        var flights = await _flightRepository.GetAllFlights();
        return flights;
    }
        
    public async Task<int> BuyTicket(int flightId, int passengerId)
    {
        var ticketId = await _ticketRepository.Add(flightId, passengerId);

        return ticketId;
    }

    public async Task<Ticket[]> GetAllPassengerTickets(int passengerId)
    {
        var tickets = await _ticketRepository.GetAllPassengerTickets(passengerId);

        return tickets;
    }
        
    public async Task DeleteTicket(int ticketId)
    {
        await _ticketRepository.DeleteTicket(ticketId);
    }
}