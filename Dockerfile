FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY BairrosEmAlta.sln .
COPY src/BairrosEmAlta.Domain/BairrosEmAlta.Domain.csproj           src/BairrosEmAlta.Domain/
COPY src/BairrosEmAlta.Application/BairrosEmAlta.Application.csproj src/BairrosEmAlta.Application/
COPY src/BairrosEmAlta.Infrastructure/BairrosEmAlta.Infrastructure.csproj src/BairrosEmAlta.Infrastructure/
COPY src/BairrosEmAlta.API/BairrosEmAlta.API.csproj                 src/BairrosEmAlta.API/

RUN dotnet restore

COPY . .

RUN dotnet publish src/BairrosEmAlta.API/BairrosEmAlta.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "BairrosEmAlta.API.dll"]
