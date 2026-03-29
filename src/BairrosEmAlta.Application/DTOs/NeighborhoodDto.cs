namespace BairrosEmAlta.Application.DTOs;

public record NeighborhoodDto(
    Guid Id,
    string Name,
    string Status,
    double Latitude,
    double Longitude,
    int Score,
    string AnalystNote,
    NeighborhoodMetricsDto Metrics,
    IReadOnlyList<NeighborhoodTransactionDto> Transactions
);

public record NeighborhoodMetricsDto(
    double AppreciationRate,
    decimal AverageM2Price,
    int NewProjects,
    double DemandIndex
);

public record NeighborhoodTransactionDto(
    string Address,
    decimal Value,
    int Area,
    DateOnly Date
);
