using System.Text.Json.Serialization;

namespace RaspberryPresenceStatus.Models
{
    public class PresenceEntity
    {
        [JsonPropertyName("@odata.context")]
        public string DataContext { get; set; } = default!;
        public string Id { get; set; } = default!;
        public string Availability { get; set; } = default!;
        public string Activity { get; set; } = default!;
    }
}