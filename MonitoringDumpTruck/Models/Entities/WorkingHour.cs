using System.ComponentModel.DataAnnotations;

namespace MonitoringDumpTruck.Models.Entities;

public class WorkingHour
{
    [Key]
    public int Id { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public int DumpTruckId { get; set; }
    public DumpTruck DumpTruck { get; set; } = null!;
}
