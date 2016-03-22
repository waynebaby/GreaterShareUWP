using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Input.Inking;

namespace GreaterShare.Services
{
	public class DrawingService : IDrawingService
	{
		public byte[] DrawStrokeOnSolidColorBackground(IReadOnlyList<InkStroke> strokes, int width, int height, Color color )
		{														 
			CanvasDevice device = CanvasDevice.GetSharedDevice();
			CanvasRenderTarget renderTarget = new CanvasRenderTarget(device, width, height, 96);

			using (var ds = renderTarget.CreateDrawingSession())
			{
				ds.Clear(color);
				ds.DrawInk(strokes);
			}
			var stm = new InMemoryRandomAccessStream();
			renderTarget.SaveAsync(stm, CanvasBitmapFileFormat.Png).AsTask().Wait();
			var readfrom = stm.GetInputStreamAt(0).AsStreamForRead();
			var ms = new MemoryStream();
			readfrom.CopyTo(ms);
			var outputBuffer = ms.ToArray();
			return outputBuffer;
		}

		public byte[] DrawStrokeOnImageBackground(IReadOnlyList<InkStroke> strokes, byte[] backgroundImageBuffer)
		{

			var stmbuffer = new InMemoryRandomAccessStream();
			stmbuffer.AsStreamForWrite().AsOutputStream().WriteAsync(backgroundImageBuffer.AsBuffer()).AsTask().Wait();

			CanvasDevice device = CanvasDevice.GetSharedDevice();
			var canbit = CanvasBitmap.LoadAsync(device, stmbuffer, 96).AsTask().Result;


			CanvasRenderTarget renderTarget = new CanvasRenderTarget(device, canbit.SizeInPixels.Width, canbit.SizeInPixels.Height, 96);

			using (var ds = renderTarget.CreateDrawingSession())
			{
				ds.Clear(Colors.Transparent);

				if (backgroundImageBuffer != null)
				{

					ds.DrawImage(canbit);
				}

				ds.DrawInk(strokes);
			}
			var stm = new InMemoryRandomAccessStream();
			renderTarget.SaveAsync(stm, CanvasBitmapFileFormat.Png).AsTask().Wait();
			var readfrom = stm.GetInputStreamAt(0).AsStreamForRead();
			var ms = new MemoryStream();
			readfrom.CopyTo(ms);
			var outputBuffer = ms.ToArray();
			return outputBuffer;
		}

	}
}
