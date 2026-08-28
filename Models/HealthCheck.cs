using static System.Net.Mime.MediaTypeNames;

namespace ApplicationsMonitoring.API.Models
{
    public class HealthCheck
    {
        public int HealthCheckId { get; set; }

        public int ApplicationId { get; set; }

        public string Status { get; set; } = null!;

        public int? ResponseTimeMs { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime CheckedDate { get; set; }

        // Navigation property
        public Application? Application { get; set; }
    }
}
