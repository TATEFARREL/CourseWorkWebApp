namespace DAL.Entities;

public class Voucher
{
    public int Id { get; set; }
    public int TourApplicationId { get; set; }
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public string Code { get; set; }
    public bool IsUsed { get; set; } = false;

    public TourApplication TourApplication { get; set; }
}