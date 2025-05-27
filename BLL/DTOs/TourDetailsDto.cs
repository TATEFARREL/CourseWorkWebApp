namespace BLL.DTOs
{
    public class TourDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public BusDto Bus { get; set; }
        public List<AttractionDetailsDto> Attractions { get; set; } = new();
    }

    public class AttractionDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public CityDto City { get; set; }
        public CountryDto Country { get; set; }
        public int VisitOrder { get; set; }
    }
}