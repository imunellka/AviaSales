namespace AviaSalesService.Requests;

public record BuyTicketRequest(
    int FlightId,
    int PassengerId
);