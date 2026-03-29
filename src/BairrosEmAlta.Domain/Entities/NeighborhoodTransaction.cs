namespace BairrosEmAlta.Domain.Entities;

public class NeighborhoodTransaction
{
    public string Address { get; init; } = string.Empty;
    public decimal Value { get; init; }
    public int Area { get; init; }
    public DateOnly Date { get; init; }
}
