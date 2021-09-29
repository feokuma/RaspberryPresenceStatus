using RaspberryPresenceStatus.Models.Enuns;

namespace RaspberryPresenceStatus.Models
{
    public class StatusLed
    {
        public int NumLeds { get; set; }
        public PresenceStatusEnum Status { get; set; }
    }
}