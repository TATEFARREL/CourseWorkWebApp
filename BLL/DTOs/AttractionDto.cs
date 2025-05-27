using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class AttractionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Attraction name is required")]
        [StringLength(100, ErrorMessage = "Attraction name cannot be longer than 100 characters")]
        public string Name { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        public string? CityName { get; set; }
    }
}