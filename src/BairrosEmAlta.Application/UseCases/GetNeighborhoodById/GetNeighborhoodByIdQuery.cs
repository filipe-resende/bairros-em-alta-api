using BairrosEmAlta.Application.DTOs;
using MediatR;

namespace BairrosEmAlta.Application.UseCases.GetNeighborhoodById;

public record GetNeighborhoodByIdQuery(Guid Id) : IRequest<NeighborhoodDto?>;
