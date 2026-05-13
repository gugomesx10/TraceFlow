using Microsoft.EntityFrameworkCore;
using TraceFlowTraining.Infrastructure.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TraceFlowDbContext>(options =>
    options.UseNpgsql(
        "Host=localhost;Port=5433;Database=traceflowdb;Username=postgres;Password=postgres"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();