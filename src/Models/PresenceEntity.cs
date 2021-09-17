using System.Text.Json.Serialization;

namespace RaspberryPresenceStatus.Models
{
    public class PresenceEntity
    {
        [JsonPropertyName("@odata.context")]
        public string DataContext { get; set; }
        public string Id { get; set; }
        public string Availability { get; set; }
        public string Activity { get; set; }
    }
}