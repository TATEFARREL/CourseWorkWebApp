namespace DAL.Entities;

public class Bus
{
    public int Id { get; set; }
    public string LicensePlate { get; set; }
    public string? Model { get; set; }
    public int Capacity { get; set; }

    public ICollection<Tour> Tours { get; set; } = new List<Tour>();
}