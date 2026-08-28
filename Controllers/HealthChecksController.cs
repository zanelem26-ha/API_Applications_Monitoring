using ApplicationsMonitoring.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsMonitoring.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HealthChecksController : ControllerBase
    {
        private readonly ApplicationMonitoringContext _context;

        public HealthChecksController(ApplicationMonitoringContext context)
        {
            _context = context;
        }

        [HttpGet("application/{applicationId}")]
        public async Task<IActionResult> GetHealthChecksByApplication(int applicationId)
        {
            var healthChecks = await _context.HealthChecks
                .Where(h => h.ApplicationId == applicationId)
                .AsNoTracking()
                .ToListAsync();

            return Ok(healthChecks);
        }

    }
}
