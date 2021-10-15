using System.Drawing;
using Iot.Device.Graphics;

namespace RaspberryPresenceStatus.Models
{
    public class BitmapImageRPi : BitmapImage
    {
        private const int BytesPerComponent = 3;
        private const int BytesPerPixel = BytesPerComponent * 3;
        private const int ResetDelayInBytes = 30;
        public BitmapImageRPi(int width, int height)
            : base(new byte[width * height * BytesPerPixel + ResetDelayInBytes], width, height, width * BytesPerPixel)
        {
        }

        public override void SetPixel(int x, int y, Color color)
        {
            var offset = y * Stride + x * BytesPerPixel;
            Data[offset++] = _convertedBytesToWS2812B[color.G * BytesPerComponent + 0];
            Data[offset++] = _convertedBytesToWS2812B[color.G * BytesPerComponent + 1];
            Data[offset++] = _convertedBytesToWS2812B[color.G * BytesPerComponent + 2];
            Data[offset++] = _convertedBytesToWS2812B[color.R * BytesPerComponent + 0];
            Data[offset++] = _convertedBytesToWS2812B[color.R * BytesPerComponent + 1];
            Data[offset++] = _convertedBytesToWS2812B[color.R * BytesPerComponent + 2];
            Data[offset++] = _convertedBytesToWS2812B[color.B * BytesPerComponent + 0];
            Data[offset++] = _convertedBytesToWS2812B[color.B * BytesPerComponent + 1];
            Data[offset++] = _convertedBytesToWS2812B[color.B * BytesPerComponent + 2];
        }

        private static readonly byte[] _convertedBytesToWS2812B = new byte[256 * BytesPerComponent];
        static BitmapImageRPi()
        {
            for (int i = 0; i < 256; i++)
            {
                int data = 0;
                for (int j = 7; j >= 0; j--)
                {
                    data = (data << 3) | 0b100 | ((i >> j) << 1) & 2;
                }

                _convertedBytesToWS2812B[i * BytesPerComponent + 0] = unchecked((byte)(data >> 16));
                _convertedBytesToWS2812B[i * BytesPerComponent + 1] = unchecked((byte)(data >> 8));
                _convertedBytesToWS2812B[i * BytesPerComponent + 2] = unchecked((byte)(data >> 0));
            }
        }
    }
}