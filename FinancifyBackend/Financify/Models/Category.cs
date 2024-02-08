using System.Text.Json.Serialization;

namespace Financify.Models;

public class Category
{
    public string Name { get; set; }
    public string? ColorHEX { get; set; }

    // Relacionamentos
    [JsonIgnore]
    public List<Transaction> Transactions { get; set; }
}