using System.ComponentModel.DataAnnotations;

namespace MonitoringDumpTruck.Models.Entities;

public class DumpTruck
{
    [Key]
    public int Id { get; set; }
    public string? Model { get; set; }
    public DateOnly? YearIssue { get; set; }
    public string? GosNumber { get; set; }
    public string? KPP { get; set; }
    public int? LoadCapacity { get; set; }
    public int? BodyVolume { get; set; }
    public string? TOIR { get; set; }
    public int? MaxSpeed { get; set;}
    public int? FullMass { get; set;}
    public int? Mileage { get; set;}
    public int? MaxFuel { get; set;}
    public string? EngineModel { get; set;}
    public string? TireModel { get; set;}

}
