namespace BLL.DTOs
{
    public class TourAttractionDto
    {
        public int TourId { get; set; }
        public int AttractionId { get; set; }
        public int VisitOrder { get; set; }

        public TourDto? Tour { get; set; }
        public AttractionDto? Attraction { get; set; }
    }
}