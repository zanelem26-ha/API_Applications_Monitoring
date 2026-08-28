using ApplicationsMonitoring.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsMonitoring.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly ApplicationMonitoringContext _context;

    public ApplicationsController(ApplicationMonitoringContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetApplications()
    {
        var applications = await _context.Applications
            .AsNoTracking()
            .ToListAsync();

        return Ok(applications);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicationById(int id)
    {
        var application = await _context.Applications
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ApplicationId == id);

        if (application == null)
        {
            return NotFound();
        }

        return Ok(application);
    }

    [HttpPost]
    public async Task<IActionResult> CreateApplication(Models.Application application)
    {

        application.CreatedDate = DateTime.Now;

        _context.Applications.Add(application);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetApplicationById),
            new { id = application.ApplicationId },
            application);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApplication(
    int id, Models.Application application)
    {
        if (id != application.ApplicationId)
        {
            return BadRequest("Application ID does not match.");
        }

        var existingApplication = await _context.Applications
            .FindAsync(id);

        if (existingApplication == null)
        {
            return NotFound();
        }

        existingApplication.ApplicationName = application.ApplicationName;
        existingApplication.Description = application.Description;
        existingApplication.Environment = application.Environment;
        existingApplication.CurrentStatus = application.CurrentStatus;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplication(int id)
    {
        var application = await _context.Applications
            .FindAsync(id);

        if (application == null)
        {
            return NotFound();
        }

        var hasIncidents = await _context.Incidents
            .AnyAsync(i => i.ApplicationId == id);

        var hasHealthChecks = await _context.HealthChecks
            .AnyAsync(h => h.ApplicationId == id);

        if (hasIncidents || hasHealthChecks)
        {
            return Conflict(
                "The application cannot be deleted because it has related incidents or health checks.");
        }

        _context.Applications.Remove(application);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}