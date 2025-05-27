using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class TourDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tour name is required")]
        [StringLength(100, ErrorMessage = "Tour name cannot be longer than 100 characters")]
        public string Name { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Bus is required")]
        [Display(Name = "Bus")]
        public int BusId { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        [StringLength(500, ErrorMessage = "Image URL cannot be longer than 500 characters")]
        public string? ImageUrl { get; set; }

        public List<AttractionDto> Attractions { get; set; } = new();
    }
}