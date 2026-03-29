using BairrosEmAlta.Application.DTOs;
using MediatR;

namespace BairrosEmAlta.Application.UseCases.GetAllNeighborhoods;

public record GetAllNeighborhoodsQuery : IRequest<IEnumerable<NeighborhoodDto>>;
