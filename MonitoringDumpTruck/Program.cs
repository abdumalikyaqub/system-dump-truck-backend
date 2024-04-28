using Microsoft.EntityFrameworkCore;
using MonitoringDumpTruck.Models;
using MonitoringDumpTruck.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<MonitoringContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("MonitoringTruckConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (MonitoringContext db = new MonitoringContext())
{
   // db.Database.EnsureCreated();

    DumpTruck dumpTruck = new DumpTruck
    {
        Model = "Kamaz",
        YearIssue = DateOnly.FromDayNumber(25),
        GosNumber = "K209A42",
        MaxSpeed = 125
    };
    db.DumpTrucks.Add(dumpTruck);

    Filling filling = new Filling
    {
        InitialVolume = 23.7,
        CreatedAt = DateTime.UtcNow,
        DumpTruck = dumpTruck,
    };
    db.Fillings.Add(filling);

    WorkingHour workingHour = new WorkingHour
    {
        Start = DateTime.UtcNow,
        End = DateTime.UtcNow.AddHours(8),
        DumpTruck = dumpTruck,
    };
    db.WorkingHours.Add(workingHour);

    db.SaveChanges();
}

using (MonitoringContext db = new MonitoringContext())
{
    var trucks = db.DumpTrucks.ToList();
    foreach (var truck in trucks)
        Console.WriteLine($" {truck.Model} - {truck.YearIssue}");

    var works = db.WorkingHours.Include(w => w.DumpTruck).ToList();
    foreach(var work in works)
        Console.WriteLine($"{work.Start} - {work.End} - {work.DumpTruck?.GosNumber}");

    var filling = db.Fillings.Include(f => f.DumpTruck).ToList();
    foreach(var fill in filling)
        Console.WriteLine($"{fill.InitialVolume} - {fill.DumpTruck.GosNumber}");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();