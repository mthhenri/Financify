namespace Financify.DTO;

public class GoalDTO
{
    public int? GoalId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? TargetValue { get; set; } // Valor pretendido (pode ser nulo)
    public decimal? InitialValue { get; set; } // Valor inicial (pode ser nulo)
    public decimal? CurrentValue { get; set; } // Valor inicial (pode ser nulo)
    public DateTime? StartDate { get; set; }
    public DateTime? CompletionDate { get; set; } // Data de conclusão (pode ser nula)

}