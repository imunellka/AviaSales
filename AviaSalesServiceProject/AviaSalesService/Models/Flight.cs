namespace AviaSalesService.Models;

public class Flight
{
    public int Id { get; set; }
    public string Departure { get; set; }
    public string Arrival { get; set; }
    public DateTimeOffset FromTime { get; set; }
    public DateTimeOffset ToTime { get; set; }
}