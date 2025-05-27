namespace DAL.Entities;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }

    public Country Country { get; set; }
    public ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();
}