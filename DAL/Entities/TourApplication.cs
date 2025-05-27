namespace DAL.Entities;

public class TourApplication
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int TourId { get; set; }
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending";

    public User? User { get; set; }
    public Tour? Tour { get; set; }
    public Voucher? Voucher { get; set; }
}