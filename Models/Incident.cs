using static System.Net.Mime.MediaTypeNames;
using System.Text.Json.Serialization;

namespace ApplicationsMonitoring.API.Models
{
    public class Incident
    {
        public int IncidentId { get; set; }

        public int ApplicationId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Severity { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime? ResolvedDate { get; set; }

        // Navigation properties
        public Application? Application { get; set; }
        
        [JsonIgnore]
        public ICollection<IncidentComment> IncidentComments { get; set; }
            = new List<IncidentComment>();
    }
}
