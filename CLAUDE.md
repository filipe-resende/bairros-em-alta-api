# CLAUDE.md — bairros-em-alta-api

API REST em .NET 10 Minimal API para o projeto de mapa de calor imobiliário de Belo Horizonte.
Backend do [bairros-em-alta-front](https://github.com/filipe-resende/bairros-em-alta-front).

## Comandos

```bash
dotnet build                                      # compilar solução
dotnet run --project src/BairrosEmAlta.API        # dev server → http://localhost:5140
dotnet test                                       # rodar testes (quando houver)

# Docker
docker compose up --build                         # build + subir → http://localhost:8080
docker compose down                               # derrubar containers
```

## Stack

- .NET 10 Minimal API
- MediatR 14 — CQRS (queries + handlers)
- Microsoft.AspNetCore.OpenApi + Swashbuckle.AspNetCore.SwaggerUI
- Clean Architecture (Domain / Application / Infrastructure / API)

## Arquitetura

```
BairrosEmAlta.Domain          — entidades puras, sem dependências externas
BairrosEmAlta.Application     — use cases (MediatR), DTOs, INeighborhoodRepository
BairrosEmAlta.Infrastructure  — implementações concretas (mock repo, futuro EF Core)
BairrosEmAlta.API             — Minimal API, endpoints, CORS, DI wiring
```

Dependências entre projetos:
```
API → Application → Domain
API → Infrastructure → Application
```

## Arquivos principais

| Arquivo | Responsabilidade |
|---|---|
| `src/BairrosEmAlta.API/Program.cs` | Bootstrap: MediatR, InfrastructureServiceExtensions, CORS, Swagger |
| `src/BairrosEmAlta.API/Endpoints/NeighborhoodEndpoints.cs` | `GET /api/neighborhoods` e `GET /api/neighborhoods/{id}` |
| `src/BairrosEmAlta.Application/Interfaces/INeighborhoodRepository.cs` | Contrato do repositório (`GetAllAsync`, `GetByIdAsync`) |
| `src/BairrosEmAlta.Application/DTOs/NeighborhoodDto.cs` | Records de resposta: `NeighborhoodDto`, `NeighborhoodMetricsDto`, `NeighborhoodTransactionDto` |
| `src/BairrosEmAlta.Application/UseCases/` | `GetAllNeighborhoodsQuery/Handler` e `GetNeighborhoodByIdQuery/Handler` |
| `src/BairrosEmAlta.Infrastructure/Repositories/NeighborhoodMockRepository.cs` | 6 bairros de BH hardcoded; trocar por EF Core quando houver banco |
| `src/BairrosEmAlta.Infrastructure/DI/InfrastructureServiceExtensions.cs` | `AddInfrastructure()` — registra `INeighborhoodRepository` como Singleton |

## Endpoints

| Método | Rota | Retorno |
|---|---|---|
| `GET` | `/api/neighborhoods` | `NeighborhoodDto[]` |
| `GET` | `/api/neighborhoods/{id}` | `NeighborhoodDto` ou `404` |
| `GET` | `/openapi/v1.json` | OpenAPI spec |
| `GET` | `/swagger` | Swagger UI (Development) |

## CORS

Liberado para `http://localhost:4200` (Angular dev). Configurado em `Program.cs` via policy `"Angular"`.
Para produção, ajustar `WithOrigins(...)` com a URL real do front.

## Bairros mockados (IDs fixos)

| Bairro | ID | Status |
|---|---|---|
| Savassi | `a1b2c3d4-e5f6-7890-abcd-ef1234567801` | HighValorization |
| Lourdes | `a1b2c3d4-e5f6-7890-abcd-ef1234567802` | HighValorization |
| Buritis | `a1b2c3d4-e5f6-7890-abcd-ef1234567803` | Growing |
| Cidade Nova | `a1b2c3d4-e5f6-7890-abcd-ef1234567804` | Growing |
| Pampulha | `a1b2c3d4-e5f6-7890-abcd-ef1234567805` | Stable |
| Barreiro | `a1b2c3d4-e5f6-7890-abcd-ef1234567806` | Stable |

## Para conectar banco de dados real

1. Adicionar `Microsoft.EntityFrameworkCore` em `BairrosEmAlta.Infrastructure`
2. Criar `NeighborhoodDbContext` e `NeighborhoodRepository` implementando `INeighborhoodRepository`
3. Em `InfrastructureServiceExtensions.cs`, trocar o registro:
   ```csharp
   // Antes
   services.AddSingleton<INeighborhoodRepository, NeighborhoodMockRepository>();
   // Depois
   services.AddScoped<INeighborhoodRepository, NeighborhoodRepository>();
   ```
4. `Domain` e `Application` não precisam de alteração

## Postman

Arquivo `BairrosEmAlta.postman_collection.json` na raiz do projeto.
Variável `{{baseUrl}}`: `http://localhost:5140` (local) ou `http://localhost:8080` (Docker).
