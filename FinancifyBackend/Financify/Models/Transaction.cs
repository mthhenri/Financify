using System.Text.Json.Serialization;

namespace Financify.Models;

public class Transaction
{
    public int Id { get; set; }
    public Guid TransactionId { get; set; }
    public bool IsExpense { get; set; } // true if expense, false if income
    public decimal Amount { get; set; }
    public string MinimalDescription { get; set; }
    public DateTime RegisterDate { get; set; }

    // Propriedades de navegação
    public string CategoryName { get; set; }
    public Category Category { get; set; }
    public int? GoalId { get; set; } // Chave estrangeira para Goal
    [JsonIgnore]
    public Goal? Goal { get; set; } // Referência de navegação para Goal

}