using BairrosEmAlta.API.Endpoints;
using BairrosEmAlta.Application.UseCases.GetAllNeighborhoods;
using BairrosEmAlta.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetAllNeighborhoodsHandler).Assembly));

builder.Services.AddInfrastructure();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/openapi/v1.json", "BairrosEmAlta API v1"));
}

app.UseCors("Angular");

app.MapNeighborhoodEndpoints();

app.Run();
