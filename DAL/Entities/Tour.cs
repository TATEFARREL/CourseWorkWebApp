using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Tour
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public int BusId { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [ForeignKey(nameof(BusId))]
    public Bus Bus { get; set; }

    public ICollection<TourAttraction> TourAttractions { get; set; } = new List<TourAttraction>();
    public ICollection<TourApplication> TourApplications { get; set; } = new List<TourApplication>();
}