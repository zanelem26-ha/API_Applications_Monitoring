using ApplicationsMonitoring.API.Data;
using ApplicationsMonitoring.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsMonitoring.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentsController : ControllerBase
{
    private readonly ApplicationMonitoringContext _context;

 
    public IncidentsController(ApplicationMonitoringContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetIncidents()
    {
        var incidents = await _context.Incidents
            .Include(i => i.Application)
            .AsNoTracking()
            .ToListAsync();

        return Ok(incidents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIncidentsById(int id)
    {
        var incident = await _context.Incidents
            .Include(i => i.Application)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.IncidentId == id);

        if (incident == null)
        {
            return NotFound();
        }

        return Ok(incident);
    }

    [HttpPost]
    public async Task<IActionResult> CreateIncident(Incident incident)
    {

        incident.CreatedDate = DateTime.Now;

        _context.Incidents.Add(incident);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetIncidentsById),
            new { id = incident.IncidentId },
            incident);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIncident(
    int id, Incident incident)
    {
        var existingIncident = await _context.Incidents
            .FindAsync(id);

        if (existingIncident == null)
        {
            return NotFound();
        }

        existingIncident.Title = incident.Title;
        existingIncident.Status = incident.Status;
        existingIncident.Severity = incident.Severity;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncident(int id)
    {
        var incident = await _context.Incidents
            .FindAsync(id);

        if (incident == null)
        {
            return NotFound();
        }

        _context.Incidents.Remove(incident);

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpGet("application/{applicationId}")]
    public async Task<IActionResult> GetIncidentsByApplication(int applicationId)
    {
        var incidents = await _context.Incidents
            .Where(i => i.ApplicationId == applicationId)
            .AsNoTracking()
            .ToListAsync();

        return Ok(incidents);
    }
}
