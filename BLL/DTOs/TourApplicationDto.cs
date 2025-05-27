namespace BLL.DTOs;

public class TourApplicationDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public int TourId { get; set; }
    public string TourName { get; set; } = null!;
    public DateTime RequestDate { get; set; }
    public string Status { get; set; } = null!;
}