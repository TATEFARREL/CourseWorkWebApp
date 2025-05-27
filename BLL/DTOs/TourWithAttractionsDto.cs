using System.Collections.Generic;

namespace BLL.DTOs
{
    public class TourWithAttractionsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AttractionDto> Attractions { get; set; } = new();
    }
}