using BairrosEmAlta.Application.UseCases.GetAllNeighborhoods;
using BairrosEmAlta.Application.UseCases.GetNeighborhoodById;
using MediatR;

namespace BairrosEmAlta.API.Endpoints;

public static class NeighborhoodEndpoints
{
    public static void MapNeighborhoodEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/neighborhoods").WithTags("Neighborhoods");

        group.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetAllNeighborhoodsQuery(), ct);
            return Results.Ok(result);
        })
        .WithName("GetAllNeighborhoods")
        .WithSummary("Retorna todos os bairros com score e métricas de investimento");

        group.MapGet("/{id:guid}", async (Guid id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetNeighborhoodByIdQuery(id), ct);
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithName("GetNeighborhoodById")
        .WithSummary("Retorna um bairro específico pelo ID");
    }
}
