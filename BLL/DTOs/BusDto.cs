using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class BusDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "License plate is required")]
        [StringLength(20, ErrorMessage = "License plate cannot be longer than 20 characters")]
        public string LicensePlate { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Model cannot be longer than 50 characters")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 100")]
        public int Capacity { get; set; }
    }
}