using RaspberryPresenceStatus.Models.Enuns;

namespace RaspberryPresenceStatus.Services
{
    public interface IDisplayService
    {
        void DrawBytes(byte[] data);
        void Clear();
    }
}