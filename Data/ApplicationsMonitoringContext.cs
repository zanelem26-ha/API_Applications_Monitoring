using ApplicationsMonitoring.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsMonitoring.API.Data
{
    public class ApplicationMonitoringContext : DbContext
    {
        public ApplicationMonitoringContext(
            DbContextOptions<ApplicationMonitoringContext> options)
            : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Incident> Incidents { get; set; }

        public DbSet<HealthCheck> HealthChecks { get; set; }

        public DbSet<IncidentComment> IncidentComments { get; set; }
    }
}
