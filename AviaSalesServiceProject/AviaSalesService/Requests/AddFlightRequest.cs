using AviaSalesService.Models;

namespace AviaSalesService.Requests;

public record AddFlightRequest(
    Flight[] Flights
);