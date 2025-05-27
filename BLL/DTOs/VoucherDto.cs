namespace BLL.DTOs;

public class VoucherDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public DateTime IssueDate { get; set; }
    public bool IsUsed { get; set; }
    public int TourApplicationId { get; set; }
    public string TourName { get; set; } = null!;
    public DateTime TourStartDate { get; set; }
}