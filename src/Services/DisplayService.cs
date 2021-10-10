using System;
using System.Device.Spi;
using System.Drawing;
using Iot.Device.Graphics;
using Iot.Device.Ws28xx;
using RaspberryPresenceStatus.Models.Enuns;

namespace RaspberryPresenceStatus.Services
{
    public class DisplayService : IDisplayService, IDisposable
    {
        private readonly SpiConnectionSettings spiSettings = new(0, 0)
        {
            ClockFrequency = 2_400_000,
            Mode = SpiMode.Mode0,
            DataBitLength = 8
        };

        public int Lines { get; private set; }
        public int Columns { get; private set; }

        private readonly SpiDevice _spiDevice;
        private readonly Ws2812b _ledDisplay;

        public DisplayService()
        {
            Lines = 8;
            Columns = 8;
            _spiDevice = SpiDevice.Create(spiSettings);
            _ledDisplay = new Ws2812b(_spiDevice, Columns, Lines);
        }

        public void DrawBytes(byte[] data)
        {
            var bytesPerLed = 3;
            var ledsPerLine = 8;
            BitmapImage leds = _ledDisplay.Image;

            for (int line = 0; line < Lines; line++)
            {
                var offset = (bytesPerLed * ledsPerLine) * line;
                for (int column = 0; column < Columns; column++)
                {
                    var r = data[offset++];
                    var g = data[offset++];
                    var b = data[offset++];
                    leds.SetPixel(column, line, Color.FromArgb(r, g, b));
                }
            }

            _ledDisplay.Update();
        }

        public void Clear()
        {
            _ledDisplay.Image.Clear();
            _ledDisplay.Update();
        }

        public void DrawStatus(PresenceStatusEnum presenceStatusEnum)
        {
            byte[] statusImageBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            switch (presenceStatusEnum)
            {
                case PresenceStatusEnum.Avaliable:
                    statusImageBytes = new byte[] { 0x3c, 0x7e, 0xfb, 0xf7, 0xaf, 0xdf, 0x7e, 0x3c };
                    break;
                case PresenceStatusEnum.Away:
                    statusImageBytes = new byte[] { 0x3c, 0x6e, 0xef, 0xef, 0xef, 0xf7, 0x7e, 0x3c };
                    break;
                case PresenceStatusEnum.Busy:
                    statusImageBytes = new byte[] { 0x3c, 0x7e, 0xff, 0x81, 0x81, 0xff, 0x7e, 0x3c };
                    break;
                case PresenceStatusEnum.DoNotDisturb:
                    statusImageBytes = new byte[] { 0x3c, 0x7e, 0xff, 0x81, 0x81, 0xff, 0x7e, 0x3c };
                    break;
                case PresenceStatusEnum.Offline:
                    break;
            }

            DrawBytes(statusImageBytes);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_spiDevice != null)
                    _spiDevice.Dispose();
            }
        }
    }
}