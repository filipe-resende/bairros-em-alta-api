using BairrosEmAlta.Application.Interfaces;
using BairrosEmAlta.Domain.Entities;
using BairrosEmAlta.Domain.Enums;

namespace BairrosEmAlta.Infrastructure.Repositories;

public class NeighborhoodMockRepository : INeighborhoodRepository
{
    private static readonly IReadOnlyList<Neighborhood> _data =
    [
        new Neighborhood
        {
            Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567801"),
            Name = "Savassi",
            Status = NeighborhoodStatus.HighValorization,
            Latitude = -19.9368,
            Longitude = -43.9369,
            Score = 92,
            AnalystNote = "Alta demanda por imóveis comerciais e residenciais de alto padrão. Baixa vacância e forte liquidez.",
            Metrics = new NeighborhoodMetrics
            {
                AppreciationRate = 14.5,
                AverageM2Price = 12800,
                NewProjects = 14,
                DemandIndex = 9.2
            },
            Transactions =
            [
                new NeighborhoodTransaction { Address = "Rua Pernambuco, 450", Value = 980000, Area = 78, Date = new DateOnly(2025, 11, 10) },
                new NeighborhoodTransaction { Address = "Av. do Contorno, 5000", Value = 2100000, Area = 160, Date = new DateOnly(2025, 11, 5) }
            ]
        },
        new Neighborhood
        {
            Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567802"),
            Name = "Lourdes",
            Status = NeighborhoodStatus.HighValorization,
            Latitude = -19.9312,
            Longitude = -43.9404,
            Score = 88,
            AnalystNote = "Bairro premium com infraestrutura consolidada. Valorização impulsionada por novos empreendimentos corporativos.",
            Metrics = new NeighborhoodMetrics
            {
                AppreciationRate = 12.1,
                AverageM2Price = 13500,
                NewProjects = 11,
                DemandIndex = 8.8
            },
            Transactions =
            [
                new NeighborhoodTransaction { Address = "Av. Álvares Cabral, 300", Value = 1450000, Area = 110, Date = new DateOnly(2025, 11, 9) },
                new NeighborhoodTransaction { Address = "Rua Cláudio Manoel, 50", Value = 3200000, Area = 230, Date = new DateOnly(2025, 11, 2) }
            ]
        },
        new Neighborhood
        {
            Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567803"),
            Name = "Buritis",
            Status = NeighborhoodStatus.Growing,
            Latitude = -19.9746,
            Longitude = -43.9718,
            Score = 76,
            AnalystNote = "Expansão residencial acelerada com novos lançamentos de médio/alto padrão e melhoria de mobilidade.",
            Metrics = new NeighborhoodMetrics
            {
                AppreciationRate = 7.3,
                AverageM2Price = 8400,
                NewProjects = 8,
                DemandIndex = 7.4
            },
            Transactions =
            [
                new NeighborhoodTransaction { Address = "Rua Professor Otávio, 120", Value = 620000, Area = 74, Date = new DateOnly(2025, 11, 7) },
                new NeighborhoodTransaction { Address = "Rua Castelo de Windsor, 80", Value = 840000, Area = 98, Date = new DateOnly(2025, 10, 29) }
            ]
        },
        new Neighborhood
        {
            Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567804"),
            Name = "Cidade Nova",
            Status = NeighborhoodStatus.Growing,
            Latitude = -19.9180,
            Longitude = -43.9280,
            Score = 71,
            AnalystNote = "Bairro em transição com foco em retrofit e novos projetos mistos residencial-comercial.",
            Metrics = new NeighborhoodMetrics
            {
                AppreciationRate = 5.8,
                AverageM2Price = 7200,
                NewProjects = 6,
                DemandIndex = 7.1
            },
            Transactions =
            [
                new NeighborhoodTransaction { Address = "Rua dos Tupinambás, 70", Value = 420000, Area = 58, Date = new DateOnly(2025, 11, 4) },
                new NeighborhoodTransaction { Address = "Av. Augusto de Lima, 900", Value = 1100000, Area = 145, Date = new DateOnly(2025, 10, 23) }
            ]
        },
        new Neighborhood
        {
            Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567805"),
            Name = "Pampulha",
            Status = NeighborhoodStatus.Stable,
            Latitude = -19.8672,
            Longitude = -43.9688,
            Score = 62,
            AnalystNote = "Região turística e universitária com estabilidade de preços. Bom para renda passiva com aluguéis de temporada.",
            Metrics = new NeighborhoodMetrics
            {
                AppreciationRate = 1.9,
                AverageM2Price = 5800,
                NewProjects = 4,
                DemandIndex = 6.5
            },
            Transactions =
            [
                new NeighborhoodTransaction { Address = "Av. Otacílio Negrão de Lima, 5000", Value = 380000, Area = 65, Date = new DateOnly(2025, 11, 1) },
                new NeighborhoodTransaction { Address = "Rua Icaraí, 30", Value = 560000, Area = 90, Date = new DateOnly(2025, 10, 19) }
            ]
        },
        new Neighborhood
        {
            Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567806"),
            Name = "Barreiro",
            Status = NeighborhoodStatus.Stable,
            Latitude = -20.0128,
            Longitude = -44.0278,
            Score = 54,
            AnalystNote = "Mercado popular com demanda constante. Indicado para perfis conservadores buscando renda estável.",
            Metrics = new NeighborhoodMetrics
            {
                AppreciationRate = 1.2,
                AverageM2Price = 3900,
                NewProjects = 2,
                DemandIndex = 5.8
            },
            Transactions =
            [
                new NeighborhoodTransaction { Address = "Av. Afonso Vaz de Melo, 200", Value = 210000, Area = 54, Date = new DateOnly(2025, 11, 6) },
                new NeighborhoodTransaction { Address = "Rua Jatobá, 400", Value = 290000, Area = 72, Date = new DateOnly(2025, 10, 25) }
            ]
        }
    ];

    public Task<IEnumerable<Neighborhood>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IEnumerable<Neighborhood>>(_data);

    public Task<Neighborhood?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult<Neighborhood?>(_data.FirstOrDefault(n => n.Id == id));
}
