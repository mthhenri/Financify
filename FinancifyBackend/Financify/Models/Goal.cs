using System.Text.Json.Serialization;

namespace Financify.Models;

public class Goal
{
    public int Id { get; set; }
    public int GoalId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? TargetValue { get; set; } // Valor pretendido (pode ser nulo)
    public decimal? InitialValue { get; set; } // Valor inicial (pode ser nulo)
    public DateTime StartDate { get; set; }
    public DateTime? CompletionDate { get; set; } // Data de conclusão (pode ser nula)

    // Propriedades de navegação
    [JsonIgnore]
    public List<Transaction> Transactions { get; set; } // Uma Goal tem muitas Transactions
}