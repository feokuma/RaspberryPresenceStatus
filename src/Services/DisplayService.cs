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
        private readonly SpiDevice _spiDevice;
        private readonly Ws2812b _ledDisplay;

        public DisplayService()
        {
            _spiDevice = SpiDevice.Create(spiSettings);
            _ledDisplay = new Ws2812b(_spiDevice, 3, 4);
        }

        public void DrawBytes(byte[] data)
        {
            BitmapImage leds = _ledDisplay.Image;

            for (int line = 0; line < 4; line++)
            {
                var mask = 0x80;
                for (int column = 0; column < 3; column++)
                {
                    if ((data[line] & mask) != 0)
                        leds.SetPixel(line, column, Color.FromArgb(0, 10, 0));
                    else
                        leds.SetPixel(line, column, Color.FromArgb(0, 0, 0));
                    mask >>= 1;
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