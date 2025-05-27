using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class CountryDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Country name is required")]
    [StringLength(100, ErrorMessage = "Country name cannot be longer than 100 characters")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
    public string? Description { get; set; }
}