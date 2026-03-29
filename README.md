# BairrosEmAlta API

API REST em .NET 10 Minimal API para o projeto de mapa de calor imobiliário de Belo Horizonte.

## Stack

- **.NET 10** — Minimal API
- **MediatR** — Use cases (CQRS)
- **Clean Architecture** — Domain / Application / Infrastructure / API
- **Swagger/OpenAPI** — Documentação interativa
- **Docker** — Dockerfile multi-stage + docker-compose

## Estrutura

```
src/
├── BairrosEmAlta.Domain/           # Entidades e enums — sem dependências externas
│   ├── Entities/
│   │   ├── Neighborhood.cs
│   │   ├── NeighborhoodMetrics.cs
│   │   └── NeighborhoodTransaction.cs
│   └── Enums/
│       └── NeighborhoodStatus.cs   # HighValorization | Growing | Stable
│
├── BairrosEmAlta.Application/      # Use cases, DTOs, interfaces de repositório
│   ├── DTOs/
│   ├── Interfaces/
│   │   └── INeighborhoodRepository.cs
│   └── UseCases/
│       ├── GetAllNeighborhoods/
│       └── GetNeighborhoodById/
│
├── BairrosEmAlta.Infrastructure/   # Implementações concretas
│   ├── DI/
│   └── Repositories/
│       └── NeighborhoodMockRepository.cs
│
└── BairrosEmAlta.API/              # Entry point — endpoints, CORS, Swagger
    ├── Endpoints/
    │   └── NeighborhoodEndpoints.cs
    └── Program.cs
```

## Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/api/neighborhoods` | Lista todos os bairros com score e métricas |
| `GET` | `/api/neighborhoods/{id}` | Retorna um bairro pelo ID (404 se não encontrar) |

## Como rodar

### Local

```bash
dotnet run --project src/BairrosEmAlta.API
# http://localhost:5140
# Swagger: http://localhost:5140/swagger
```

### Docker

```bash
docker compose up --build
# http://localhost:8080
# Swagger: http://localhost:8080/swagger
```

## Postman

Importe o arquivo `BairrosEmAlta.postman_collection.json` no Postman.
A variável `{{baseUrl}}` aponta para `http://localhost:5140` por padrão — altere para `http://localhost:8080` ao usar Docker.

---

## Arquitetura do Sistema

Visão completa do fluxo planejado — da requisição do cliente até a resposta com dados processados via event-driven architecture.

```mermaid
---
config:
  layout: fixed
---
flowchart TB
    A["CLIENTE"] --> B["GET /bairro?nome=buritis"]
    B --> C["API (.NET)"]
    C --> C1["Valida Request"] & C2["Gera CorrelationId"] & D["Envia mensagem para FILA<br>(RabbitMQ / Service Bus)"]
    D --> E["Orquestrador / Consumer"]
    E --> E1["Recebe requisição"] & F["Publica evento"]
    F --> G["Kafka<br>topic: buscar-dados-externos"]
    G --> H["Serviço: Search Data Collector"]
    H --> H1["Busca notícias"] & H2["Consulta APIs externas"] & I["Publica evento"]
    I --> J["Kafka<br>topic: dados-coletados"]
    K["Kafka<br>topic: dados-processados"] --> M["Serviço Score<br>(topic: dados-processados)"] & N["Futuros Serviços<br>(ex: enriquecimento)"]
    M --> O["Kafka<br>topic: dados-processados"]
    N --> O
    O --> P["Aggregator Service"]
    P --> P1["Junta resultados"] & P2["Salva no banco"] & P3["Atualiza cache"] & Q["Banco de Dados + Redis"]
    Q --> R["API responde<br>(Polling / Consulta)"]
    J --> L["Serviço: Extract Data<br>"]
    L --> n1["Tratamento de Dados NLP"] & n2["Publicar"]
    n2 --> K
```
