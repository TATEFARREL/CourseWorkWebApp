namespace DAL.Entities;

public class TourAttraction
{
    public int TourId { get; set; }
    public int AttractionId { get; set; }
    public int VisitOrder { get; set; }

    public Tour Tour { get; set; }
    public Attraction Attraction { get; set; }
}