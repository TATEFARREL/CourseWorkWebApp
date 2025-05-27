using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "City name is required")]
        [StringLength(100, ErrorMessage = "City name cannot be longer than 100 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Country is required")]
        public int CountryId { get; set; }

        public string? CountryName { get; set; }
    }
}