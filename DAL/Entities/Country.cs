namespace DAL.Entities;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<City> Cities { get; set; } = new List<City>();
}