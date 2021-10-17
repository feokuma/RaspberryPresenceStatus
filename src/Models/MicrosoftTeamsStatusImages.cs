using System.Collections.Generic;
using System.Drawing;
using RaspberryPresenceStatus.Extensions;
using RaspberryPresenceStatus.Models.Enuns;

namespace RaspberryPresenceStatus.Models
{
    public class MicrosoftTeamsStatusImages
    {
        private const int WIDTH = 8;
        private const int HEIGTH = 8;
        private const int BYTES_PER_COMPONENT = 3;

        private static readonly Color AVALIABLE_COLOR = Color.FromArgb(0x00, 0x10, 0x00);
        private static readonly Color BUSY_COLOR = Color.FromArgb(0x10, 0x00, 0x00);
        private static readonly Color AWAY_COLOR = Color.FromArgb(0x10, 0x10, 0x00);

        public static byte[] StatusImageFromEnum(PresenceStatusEnum statusEnum)
        {
            return statusEnum switch
            {
                PresenceStatusEnum.Avaliable => ConvertBytesToBitmapImageRPi(new byte[] { 0x3c, 0x7e, 0xfb, 0xf7, 0xaf, 0xdf, 0x7e, 0x3c }, AVALIABLE_COLOR),
                PresenceStatusEnum.Away => ConvertBytesToBitmapImageRPi(new byte[] { 0x3c, 0x6e, 0xef, 0xef, 0xef, 0xf7, 0x7e, 0x3c }, AWAY_COLOR),
                PresenceStatusEnum.Busy => ConvertBytesToBitmapImageRPi(new byte[] { 0x3c, 0x7e, 0xff, 0xff, 0xff, 0xff, 0x7e, 0x3c }, BUSY_COLOR),
                PresenceStatusEnum.DoNotDisturb => ConvertBytesToBitmapImageRPi(new byte[] { 0x3c, 0x7e, 0xff, 0x81, 0x81, 0xff, 0x7e, 0x3c }, BUSY_COLOR),
                _ => ConvertBytesToBitmapImageRPi(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Color.Black),
            };
        }

        private static byte[] ConvertBytesToBitmapImageRPi(byte[] pixelsBytes, Color color)
        {
            var imageBytes = new List<byte>();

            for (int y = 0; y < HEIGTH; y++)
            {
                var mask = 0b10000000;
                for (int x = 0; x < WIDTH; x++)
                {
                    var ledStatus = pixelsBytes[y] & mask;
                    if (ledStatus > 0)
                        imageBytes.AddRange(color.ToRgbBytes());
                    else
                        imageBytes.AddRange(Color.Black.ToRgbBytes());

                    mask >>= 1;
                }
            }
            return imageBytes.ToArray();
        }
    }
}