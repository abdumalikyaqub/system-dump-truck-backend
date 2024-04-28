using Microsoft.EntityFrameworkCore;
using MonitoringDumpTruck.Models.Entities;

namespace MonitoringDumpTruck.Models;

public class MonitoringContext : DbContext
{
    public DbSet<DumpTruck> DumpTrucks { get; set; } = null!;
    public DbSet<WorkingHour> WorkingHours { get; set; } = null!;
    public DbSet<Filling> Fillings { get; set; } = null!;

    //public MonitoringContext(DbContextOptions<MonitoringContext> options)
    //    : base(options)
    //{
    //    Database.EnsureCreated();
    //    bool isAvalaible = Database.CanConnect();
    //    // bool isAvalaible2 = await db.Database.CanConnectAsync();
    //    if (isAvalaible) Console.WriteLine("База данных доступна");
    //    else Console.WriteLine("База данных не доступна");

    //}
    
    public MonitoringContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=monitoring-truck;Username=postgres;Password=malik98");
        optionsBuilder
            .UseLazyLoadingProxies();        // подключение lazy loading
        
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
