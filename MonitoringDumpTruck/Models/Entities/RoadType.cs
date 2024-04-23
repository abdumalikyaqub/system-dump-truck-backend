﻿using System.ComponentModel.DataAnnotations;

namespace MonitoringDumpTruck.Models.Entities;

public class RoadType
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Pointer> Pointers { get; set; } = [];

}
