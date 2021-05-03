using RaspberryPresenceStatus.Models.Enuns;

namespace RaspberryPresenceStatus.Services
{
    public interface IDisplayService
    {
        void SetStatus(PresenceStatusEnum presenceStatus);
    }
}