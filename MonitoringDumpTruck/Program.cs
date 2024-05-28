using Microsoft.EntityFrameworkCore;
using MonitoringDumpTruck.Models;
using MonitoringDumpTruck.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MonitoringContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MonitoringTruckConnection")));

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