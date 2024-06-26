﻿using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoringDumpTruck.Models;
using MonitoringDumpTruck.Models.Entities;

namespace MonitoringDumpTruck.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DumpTrucksController : ControllerBase
{
    private readonly MonitoringContext _context;

    public DumpTrucksController(MonitoringContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DumpTruck>>> GetDumpTrucks()
    {
        return await _context.DumpTrucks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DumpTruck>> GetDumpTruck(int id)
    {
        var dumpTruck = await _context.DumpTrucks.FindAsync(id);

        if (dumpTruck == null)
        {
            return NotFound();
        }

        return dumpTruck;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDumpTruck(int id, DumpTruck dumpTruck)
    {
        if (id != dumpTruck.Id)
        {
            return BadRequest();
        }

        _context.Entry(dumpTruck).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DumpTruckExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<DumpTruck>> PostDumpTruck(DumpTruck dumpTruck)
    {
        _context.DumpTrucks.Add(dumpTruck);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDumpTruck", new { id = dumpTruck.Id }, dumpTruck);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDumpTruck(int id)
    {
        var dumpTruck = await _context.DumpTrucks.FindAsync(id);
        if (dumpTruck == null)
        {
            return NotFound();
        }

        _context.DumpTrucks.Remove(dumpTruck);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DumpTruckExists(int id)
    {
        return _context.DumpTrucks.Any(e => e.Id == id);
    }


    [HttpPost("import/json")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> ImportFromJson(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        using (var streamReader = new StreamReader(file.OpenReadStream()))
        {
            var jsonData = await streamReader.ReadToEndAsync();
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            var dumpTrucks = JsonSerializer.Deserialize<List<DumpTruck>>(jsonData, options);

            if (dumpTrucks != null)
            {
                foreach (var item in dumpTrucks)
                {
                    var truck = new DumpTruck
                    {
                        MaxFuel = item.MaxFuel,
                        MaxSpeed = item.MaxSpeed,
                        Mileage = item.Mileage,
                        Model = item.Model,
                        EngineModel = item.EngineModel,
                        BodyVolume = item.BodyVolume,
                        YearIssue = item.YearIssue,
                        GosNumber = item.GosNumber,
                        KPP = item.KPP,
                        LoadCapacity = item.LoadCapacity,
                        TOIR = item.TOIR,
                        FullMass = item.FullMass,
                        TireModel = item.TireModel,
                    };
                    _context.DumpTrucks.Add(truck);
                }
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Data imported successfully");
    }


    [HttpPost("export/json")]
    public async Task<IActionResult> ExportToJson()
    {
        var dumpTrucks = await _context.DumpTrucks.ToListAsync();
        var options = new JsonSerializerOptions
        {
            
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true 
        };
        var jsonData = JsonSerializer.Serialize(dumpTrucks, options); 
        var fileName = "dump_trucks.json";

        return File(Encoding.UTF8.GetBytes(jsonData), "application/json", fileName);
    }

}
