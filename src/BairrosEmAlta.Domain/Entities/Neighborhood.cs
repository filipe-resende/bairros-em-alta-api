using BairrosEmAlta.Domain.Enums;

namespace BairrosEmAlta.Domain.Entities;

public class Neighborhood
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public NeighborhoodStatus Status { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public int Score { get; init; }
    public string AnalystNote { get; init; } = string.Empty;
    public NeighborhoodMetrics Metrics { get; init; } = new();
    public IReadOnlyList<NeighborhoodTransaction> Transactions { get; init; } = [];
}
