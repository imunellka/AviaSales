using AviaSalesService.Models;

namespace AviaSalesService.Responses;

public record GetAllFlightsResponse(
    Flight[] Flights
);