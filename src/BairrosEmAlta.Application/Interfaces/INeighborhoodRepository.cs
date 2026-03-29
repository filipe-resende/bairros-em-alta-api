using BairrosEmAlta.Domain.Entities;

namespace BairrosEmAlta.Application.Interfaces;

public interface INeighborhoodRepository
{
    Task<IEnumerable<Neighborhood>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Neighborhood?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
