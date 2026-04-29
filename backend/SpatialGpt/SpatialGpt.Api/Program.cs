using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SpatialGpt.Api.Data;
using SpatialGpt.Api.Endpoints;
using SpatialGpt.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.UseNetTopologySuite()
    )
);

builder.Services.AddScoped<QueryParser>();
builder.Services.AddScoped<PropertyQueryService>();

// CORS — allow Angular dev server
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("AllowAngular");
app.UseHttpsRedirection();

// Map endpoints
app.MapQueryEndpoints();

app.Run();