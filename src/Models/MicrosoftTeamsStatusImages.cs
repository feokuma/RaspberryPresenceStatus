using System.Collections.Generic;
using RaspberryPresenceStatus.Models.Enuns;

namespace RaspberryPresenceStatus.Models
{
    public class MicrosoftTeamsStatusImages
    {
        public const int WIDTH = 8;
        public const int HEIGTH = 8;
        public const int BYTES_PER_COMPONENT = 3;


        public static byte[] StatusImageFromEnum(PresenceStatusEnum statusEnum)
        {
            return statusEnum switch
            {
                PresenceStatusEnum.Avaliable => ConvertBytesToBitmapImageRPi(new byte[] { 0x3c, 0x7e, 0xfb, 0xf7, 0xaf, 0xdf, 0x7e, 0x3c }),
                PresenceStatusEnum.Away => ConvertBytesToBitmapImageRPi(new byte[] { 0x3c, 0x6e, 0xef, 0xef, 0xef, 0xf7, 0x7e, 0x3c }),
                PresenceStatusEnum.Busy => ConvertBytesToBitmapImageRPi(new byte[] { 0x3c, 0x7e, 0xff, 0xff, 0xff, 0xff, 0x7e, 0x3c }),
                PresenceStatusEnum.DoNotDisturb => ConvertBytesToBitmapImageRPi(new byte[] { 0x3c, 0x7e, 0xff, 0x81, 0x81, 0xff, 0x7e, 0x3c }),
                _ => ConvertBytesToBitmapImageRPi(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }),
            };
        }

        private static byte[] ConvertBytesToBitmapImageRPi(byte[] pixelsBytes)
        {
            var imageBytes = new List<byte>();

            for (int y = 0; y < HEIGTH; y++)
            {
                var mask = 0b10000000;
                for (int x = 0; x < WIDTH; x++)
                {
                    var ledStatus = pixelsBytes[y] & mask;
                    if (ledStatus > 0)
                        imageBytes.AddRange(new byte[] { 0x00, 0x01, 0x00 });
                    else
                        imageBytes.AddRange(new byte[] { 0x00, 0x00, 0x00 });

                    mask >>= 1;
                }
            }
            return imageBytes.ToArray();
        }
    }
}