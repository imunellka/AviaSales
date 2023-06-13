using AviaSalesService.Models;

namespace AviaSalesService.Responses;

public record GetAllPassengerTicketsResponse(
    Ticket[] Tickets
);