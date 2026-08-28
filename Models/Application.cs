using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json.Serialization;

namespace ApplicationsMonitoring.API.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; } = null!;

        public string? Description { get; set; }

        public string Environment { get; set; } = null!;

        public string CurrentStatus { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        // Navigation properties
        [JsonIgnore]
        public ICollection<Incident> Incidents { get; set; } = new List<Incident>();

        [JsonIgnore]
        public ICollection<HealthCheck> HealthChecks { get; set; } = new List<HealthCheck>();

        
    }
}
