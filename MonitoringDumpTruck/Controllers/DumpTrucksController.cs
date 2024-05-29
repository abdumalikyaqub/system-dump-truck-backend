using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
}
