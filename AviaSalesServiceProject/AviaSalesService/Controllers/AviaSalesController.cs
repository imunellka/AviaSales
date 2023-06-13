using AviaSalesService.Models;
using AviaSalesService.Repositories.Migrations;
using AviaSalesService.Requests;
using AviaSalesService.Responses;
using AviaSalesService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AviaSalesService.Controllers;

[ApiController]
[Route("avia-sales-controller")]
public class AviaSalesController : ControllerBase
{
    private readonly AviaSalesHelperService _aviaSalesService;
    
    public AviaSalesController(AviaSalesHelperService aviaSalesService)
    {
        _aviaSalesService = aviaSalesService;
    }
    
    [HttpPost]
    [Route("flights")]
    public async Task<IActionResult> AddFlights(AddFlightRequest request)
    {
        var flightsIds = await _aviaSalesService.AddFlights(request.Flights);
        
        var stringIds = "";
        foreach (var item in flightsIds)
        {
            stringIds += item + " ";
        }
        
        return StatusCode(StatusCodes.Status200OK, $"Flights created with id {stringIds}.");
    }
    
    [HttpGet]
    [Route("flights")]
    public async Task<GetAllFlightsResponse> GetAllFlights()
    {
        var flights = await _aviaSalesService.GetAllFlights();
        
        return new GetAllFlightsResponse(flights);
    }

    [HttpPost]
    [Route("tickets")]
    public async Task<IActionResult> BuyTicket(BuyTicketRequest request)
    {
        var flight = await _aviaSalesService.GetAllFlights();

        var currentFligt = flight.Where(x => x.Id == request.FlightId).FirstOrDefault();

        if (currentFligt == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, $"Flight with this id doesn't exist.");
        }
        
        var ticketId = await _aviaSalesService.BuyTicket(request.FlightId, request.PassengerId);
        
        return StatusCode(StatusCodes.Status200OK, $"You but ticket with id {ticketId}.");
    }
    
    [HttpGet]
    [Route("tickets")]
    public async Task<IEnumerable<Ticket>> GetAllPassengerTickets([FromQuery]GetAllPassengerTicketsRequest request)
    {
        var tickets = await _aviaSalesService.GetAllPassengerTickets(request.PassengerId);

        var activeTickets = tickets.Where(x => x.Active);
        
        return tickets;
    }
    
    [HttpDelete]
    [Route("tickets")]
    public async Task DeleteTicket(DeleteTicketRequest request)
    {
        await _aviaSalesService.DeleteTicket(request.TicketId);
    }
}