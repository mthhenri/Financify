
namespace Financify.DTO;

public class TransactionDTO
{
    public Guid? TransactionId { get; set; }
    public bool? IsExpense { get; set; } // true if expense, false if income
    public decimal? Amount { get; set; }
    public string? CategoryName { get; set; }
    public string? MinimalDescription { get; set; }
    public DateTime? RegisterDate { get; set; }
    public int? MonthNumber { get; set; }
    public int? YearNumber { get; set; }
    public int? GoalId { get; set; }
}