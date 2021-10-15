using System.Drawing;
using Iot.Device.Graphics;

namespace RaspberryPresenceStatus.Models
{
	public class BitmapImageRPi : BitmapImage
	{
		public BitmapImageRPi(byte[] data, int width, int height, int stride)
			: base(data, width, height, stride)
		{
		}

		public override void SetPixel(int x, int y, Color color)
		{
			throw new System.NotImplementedException();
		}
	}
}