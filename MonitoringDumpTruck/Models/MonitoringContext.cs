using Microsoft.EntityFrameworkCore;
using MonitoringDumpTruck.Models.Entities;

namespace MonitoringDumpTruck.Models;

public class MonitoringContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<DumpTruck> DumpTrucks { get; set; } = null!;
    public DbSet<RoadType> RoadTypes { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<WorkingHour> WorkingHours { get; set; } = null!;
    public DbSet<Filling> Fillings { get; set; } = null!;
    public DbSet<Pointer> Pointers { get; set; } = null!;

    public MonitoringContext(DbContextOptions<MonitoringContext> options)
        : base(options)
    {
        //Database.EnsureCreated();
        bool isAvalaible = Database.CanConnect();
        // bool isAvalaible2 = await db.Database.CanConnectAsync();
        if (isAvalaible) Console.WriteLine("База данных доступна");
        else Console.WriteLine("База данных не доступна");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure default schema
        modelBuilder.HasDefaultSchema("mining_truck");

        //// использование Fluent API
        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Role>().ToTable("roles");
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<DumpTruck>().ToTable("dump_trucks");
        modelBuilder.Entity<Filling>().ToTable("fillings");
        modelBuilder.Entity<RoadType>().ToTable("road_types");
        modelBuilder.Entity<Status>().ToTable("statutes");
        modelBuilder.Entity<WorkingHour>().ToTable("working_hours");
        modelBuilder.Entity<Pointer>().ToTable("pointers");
    }
}
