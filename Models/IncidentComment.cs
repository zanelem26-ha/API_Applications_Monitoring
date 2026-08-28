using System.Text.Json.Serialization;

namespace ApplicationsMonitoring.API.Models
{
    public class IncidentComment
    {
        public int IncidentCommentId { get; set; }

        public int IncidentId { get; set; }

        public string Comment { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        // Navigation property
        [JsonIgnore]
        public Incident ? Incident { get; set; }
    }
}
