using System.ComponentModel.DataAnnotations;

namespace MonitoringDumpTruck.Models.Entities;

public class Filling
{
    [Key]
    public int Id { get; set; }
    public double? InitialVolume { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int DumpTruckId { get; set; }
    public DumpTruck DumpTruck { get; set; } = null!;
}
