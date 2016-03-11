using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using System.IO;
namespace GreaterShare.Services
{
	public class ConvertImageToPNGService : IImageConvertService
	{
		public async Task ConverterBitmapToTargetStreamAsync(IRandomAccessStream bitmapSourceStream, IRandomAccessStream saveTargetStream)
		{
			BitmapDecoder decoder = await BitmapDecoder.CreateAsync(bitmapSourceStream);
			// Scale image to appropriate size
			BitmapTransform transform = new BitmapTransform()
			{
				//ScaledWidth = Convert.ToUInt32(bi.PixelWidth),
				//ScaledHeight = Convert.ToUInt32(bi.PixelHeight) 
			};

			PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
				BitmapPixelFormat.Bgra8,    // WriteableBitmap uses BGRA format
				BitmapAlphaMode.Straight,
				transform,
				ExifOrientationMode.RespectExifOrientation, // This sample ignores Exif orientation
				ColorManagementMode.DoNotColorManage);

			var BitmapEncoderGuid = BitmapEncoder.PngEncoderId;

			BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoderGuid, saveTargetStream);

			encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
					  (uint)decoder.PixelWidth,
					  (uint)decoder.PixelHeight,
					  96.0,
					  96.0,
					  pixelData.DetachPixelData());
			await encoder.FlushAsync();
		}

	}
}
