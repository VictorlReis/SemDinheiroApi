namespace SemDinheiroapi.Databases.Models;

public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public int Month { get; set; }
    public string PaymentMethod { get; set; }
    public string UserId { get; set; }
}
