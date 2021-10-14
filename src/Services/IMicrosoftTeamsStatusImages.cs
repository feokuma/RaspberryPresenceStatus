using RaspberryPresenceStatus.Models.Enuns;

namespace RaspberryPresenceStatus.Services
{
    public interface IMicrosoftTeamsStatusImages
    {
        byte[] StatusImageFromEnum(PresenceStatusEnum statusEnum);
    }
}