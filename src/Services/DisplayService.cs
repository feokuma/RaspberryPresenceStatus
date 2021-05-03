using System.Drawing;
using System;
using System.Device.Spi;
using Iot.Device.Graphics;
using Iot.Device.Ws28xx;
using RaspberryPresenceStatus.Models.Enuns;

namespace RaspberryPresenceStatus.Services
{
    public class DisplayService : IDisplayService, IDisposable
    {
        private SpiConnectionSettings spiSettings = new SpiConnectionSettings(0, 0)
        {
            ClockFrequency = 2_400_000,
            Mode = SpiMode.Mode0,
            DataBitLength = 8
        };
        private SpiDevice _spiDevice;
        private Ws2812b _ledDisplay;

        public DisplayService()
        {
            _spiDevice = SpiDevice.Create(spiSettings);
            _ledDisplay = new Ws2812b(_spiDevice, 8, 8);
        }

        public void Dispose()
        {
            _spiDevice.Dispose();
        }

        public void SetImage(BitmapImage bitmapImage)
        {

        }

        public void SetStatus(PresenceStatusEnum presenceStatus)
        {
            switch (presenceStatus)
            {
                case PresenceStatusEnum.Avaliable:
                    DrawAvaliable();
                    break;
            }
        }

        private void DrawAvaliable()
        {
            BitmapImage leds = _ledDisplay.Image;
            byte[] avaliable = { 0x3c, 0x7e, 0xfb, 0xf7, 0xaf, 0xdf, 0x7e, 0x3c };
            byte mask = 0x80;

            for (int line = 0; line < 8; line++)
            {
                mask = 0x80;
                for (int column = 0; column < 8; column++)
                {
                    if ((avaliable[line] & mask) == 1)
                        leds.SetPixel(line, column, Color.FromArgb(0, 10, 0));
                    else
                        leds.SetPixel(line, column, Color.FromArgb(0, 0, 0));
                    mask >>= 1;
                }
            }

            _ledDisplay.Update();
        }
    }
}