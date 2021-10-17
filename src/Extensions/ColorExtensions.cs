using System.Drawing;

namespace RaspberryPresenceStatus.Extensions
{
    public static class ColorExtensions
    {
        public static byte[] ToRgbBytes(this Color color)
        {
            return new byte[] { color.R, color.G, color.B };
        }
    }
}