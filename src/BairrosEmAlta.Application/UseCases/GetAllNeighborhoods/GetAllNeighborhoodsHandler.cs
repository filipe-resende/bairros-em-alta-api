using BairrosEmAlta.Application.DTOs;
using BairrosEmAlta.Application.Interfaces;
using BairrosEmAlta.Domain.Entities;
using MediatR;

namespace BairrosEmAlta.Application.UseCases.GetAllNeighborhoods;

public class GetAllNeighborhoodsHandler(INeighborhoodRepository repository)
    : IRequestHandler<GetAllNeighborhoodsQuery, IEnumerable<NeighborhoodDto>>
{
    public async Task<IEnumerable<NeighborhoodDto>> Handle(
        GetAllNeighborhoodsQuery request,
        CancellationToken cancellationToken)
    {
        var neighborhoods = await repository.GetAllAsync(cancellationToken);
        return neighborhoods.Select(MapToDto);
    }

    private static NeighborhoodDto MapToDto(Neighborhood n) => new(
        n.Id,
        n.Name,
        n.Status.ToString(),
        n.Latitude,
        n.Longitude,
        n.Score,
        n.AnalystNote,
        new NeighborhoodMetricsDto(
            n.Metrics.AppreciationRate,
            n.Metrics.AverageM2Price,
            n.Metrics.NewProjects,
            n.Metrics.DemandIndex),
        n.Transactions.Select(t => new NeighborhoodTransactionDto(t.Address, t.Value, t.Area, t.Date)).ToList()
    );
}
