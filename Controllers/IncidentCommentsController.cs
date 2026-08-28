using ApplicationsMonitoring.API.Data;
using ApplicationsMonitoring.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsMonitoring.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentCommentsController : ControllerBase
    {
        private readonly ApplicationMonitoringContext _context;

        public IncidentCommentsController(ApplicationMonitoringContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetIncidentComments()
        {
            var comments = await _context.IncidentComments
                .Include(i => i.Incident)
                .AsNoTracking()
                .ToListAsync();

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _context.IncidentComments
                .Include(i => i.Incident)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IncidentCommentId == id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }


        [HttpPost]
        public async Task<IActionResult> CreateIncidentComment(IncidentComment comment)
        {
            var incident = await _context.Incidents
                .FindAsync(comment.IncidentId);

            if (incident == null)
            {
                return BadRequest("Incident does not exist.");
            }

            comment.CreatedDate = DateTime.Now;

            _context.IncidentComments.Add(comment);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCommentById),
                new { id = comment.IncidentCommentId },
                comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditIncidentComment(
        int id, IncidentComment incidentComment)
        {
                if (id != incidentComment.IncidentCommentId)
                {
                    return BadRequest("IncindentComment ID does not match.");
                }

                var existingComment = await _context.IncidentComments
                    .FindAsync(id);

                if (existingComment == null)
                {
                    return NotFound();
                }

            existingComment.Comment = incidentComment.Comment;
            existingComment.CreatedBy = incidentComment.CreatedBy;

            await _context.SaveChangesAsync();

                return NoContent();
        }


    }
}
