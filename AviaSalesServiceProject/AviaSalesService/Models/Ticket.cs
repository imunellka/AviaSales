namespace AviaSalesService.Models;

public class Ticket
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public bool Active { get; set; }
}