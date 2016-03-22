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
		public byte[] DrawStrokeOnBackground(IReadOnlyList<InkStroke> strokes, int width, int height, byte[] backgroundImageBuffer)
		{
			CanvasDevice device = CanvasDevice.GetSharedDevice();
			CanvasRenderTarget renderTarget = new CanvasRenderTarget(device, width, height, 96);

			using (var ds = renderTarget.CreateDrawingSession())
			{
				ds.Clear(Colors.Transparent);

				if (backgroundImageBuffer != null)
				{
					var stmbuffer = new InMemoryRandomAccessStream();
					stmbuffer.AsStreamForWrite().AsOutputStream().WriteAsync(backgroundImageBuffer.AsBuffer()).AsTask().Wait();
					var canbit = CanvasBitmap.LoadAsync(ds, stmbuffer, 96).AsTask().Result;
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
