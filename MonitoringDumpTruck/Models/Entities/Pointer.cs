using System.ComponentModel.DataAnnotations;

namespace MonitoringDumpTruck.Models.Entities;

public class Pointer
{
    [Key]
    public int Id { get; set; }
    public int DumpTruckId { get; set; }
    public int RoadTypeId { get; set; }
    public int StatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public double Speed { get; set; }
    public double Fuel { get; set; }
    public double EngineTemperature { get; set; }
    public double EnginePressure { get; set; }
    public double EngineSpeed { get; set; }
    public double EngineLoad { get; set; }
    public double EngineVibration { get; set; }
    public double TirePressure { get; set; }
    public double TireTemperature { get; set; }
    public double TireTreadDepth { get; set; }

    public DumpTruck DumpTruck { get; set; } = null!;
    public RoadType RoadType { get; set; } = null!;
    public Status Status { get; set; } = null!;

}