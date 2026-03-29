using BairrosEmAlta.Application.DTOs;
using BairrosEmAlta.Application.Interfaces;
using BairrosEmAlta.Domain.Entities;
using MediatR;

namespace BairrosEmAlta.Application.UseCases.GetNeighborhoodById;

public class GetNeighborhoodByIdHandler(INeighborhoodRepository repository)
    : IRequestHandler<GetNeighborhoodByIdQuery, NeighborhoodDto?>
{
    public async Task<NeighborhoodDto?> Handle(
        GetNeighborhoodByIdQuery request,
        CancellationToken cancellationToken)
    {
        var neighborhood = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (neighborhood is null) return null;

        return new NeighborhoodDto(
            neighborhood.Id,
            neighborhood.Name,
            neighborhood.Status.ToString(),
            neighborhood.Latitude,
            neighborhood.Longitude,
            neighborhood.Score,
            neighborhood.AnalystNote,
            new NeighborhoodMetricsDto(
                neighborhood.Metrics.AppreciationRate,
                neighborhood.Metrics.AverageM2Price,
                neighborhood.Metrics.NewProjects,
                neighborhood.Metrics.DemandIndex),
            neighborhood.Transactions
                .Select(t => new NeighborhoodTransactionDto(t.Address, t.Value, t.Area, t.Date))
                .ToList()
        );
    }
}
