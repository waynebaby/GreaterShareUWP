using Microsoft.Graphics.Canvas;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace GreaterShare.Glue
{
	public class InkCanvasControllerBehavior : Behavior<InkCanvas>
	{

		protected override void OnAttached()
		{
			base.OnAttached();

			try
			{


				RegisterPropertyChangedAndSaveUnregToken(
				   IsInputEnabledProperty,
				   (o, a) => this.InkPresenter.IsInputEnabled = IsInputEnabled);

				RegisterPropertyChangedAndSaveUnregToken(
					InputDeviceTypesProperty,
					(o, a) => this.InkPresenter.InputDeviceTypes = InputDeviceTypes);

				DependencyPropertyChangedCallback detailChanged = (o, a) =>
				 {
					 var att = InkPresenter.CopyDefaultDrawingAttributes();
					 att.Color = Color;
					 att.PenTip = PenTipShape.Circle;
					 att.IgnorePressure = false;
					 att.DrawAsHighlighter = DrawAsHighlighter;
					 att.Size = new Windows.Foundation.Size(StrokeSize, StrokeSize);
					 InkPresenter.UpdateDefaultDrawingAttributes(att);

				 };

				RegisterPropertyChangedAndSaveUnregToken(ColorProperty, detailChanged);
				RegisterPropertyChangedAndSaveUnregToken(StrokeSizeProperty, detailChanged);
				RegisterPropertyChangedAndSaveUnregToken(DrawAsHighlighterProperty, detailChanged);

				regs.ForEach(i => i.Item3(null, null));



			}
			catch (Exception ex)
			{

				throw;
			}


		}

		private InkPresenter InkPresenter
		{
			get { return AssociatedObject.InkPresenter; }
		}

		private void RegisterPropertyChangedAndSaveUnregToken(DependencyProperty dp, DependencyPropertyChangedCallback callback)
		{
			var regv = RegisterPropertyChangedCallback(dp, callback);
			regs.Add(Tuple.Create(dp, regv, callback));
		}

		List<Tuple<DependencyProperty, long, DependencyPropertyChangedCallback>> regs = new List<Tuple<DependencyProperty, long, DependencyPropertyChangedCallback>>();






		protected override void OnDetaching()
		{
			regs.ForEach(i => UnregisterPropertyChangedCallback(i.Item1, i.Item2));

			regs.Clear();

			base.OnDetaching();

		}



		public double StrokeSize
		{
			get { return (double)GetValue(StrokeSizeProperty); }
			set { SetValue(StrokeSizeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for StrokeSize.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty StrokeSizeProperty =
			DependencyProperty.Register(nameof(StrokeSize), typeof(double), typeof(InkCanvasControllerBehavior), new PropertyMetadata(20d));



		public Color Color
		{
			get { return (Color)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ColorProperty =
			DependencyProperty.Register(nameof(Color), typeof(Color), typeof(InkCanvasControllerBehavior), new PropertyMetadata(Colors.Red));




		public bool DrawAsHighlighter
		{
			get { return (bool)GetValue(DrawAsHighlighterProperty); }
			set { SetValue(DrawAsHighlighterProperty, value); }
		}

		// Using a DependencyProperty as the backing store for IsDrawingAsMarker.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty DrawAsHighlighterProperty =
			DependencyProperty.Register(nameof(DrawAsHighlighter), typeof(bool), typeof(InkCanvasControllerBehavior), new PropertyMetadata(false));




		public bool IsInputEnabled
		{
			get { return (bool)GetValue(IsInputEnabledProperty); }
			set { SetValue(IsInputEnabledProperty, value); }
		}

		// Using a DependencyProperty as the backing store for IsInputEnabled.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsInputEnabledProperty =
			DependencyProperty.Register(nameof(IsInputEnabled), typeof(bool), typeof(InkCanvasControllerBehavior), new PropertyMetadata(false));




		public CoreInputDeviceTypes InputDeviceTypes
		{
			get { return (CoreInputDeviceTypes)GetValue(InputDeviceTypesProperty); }
			set { SetValue(InputDeviceTypesProperty, value); }
		}

		// Using a DependencyProperty as the backing store for InputDeviceTypes.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty InputDeviceTypesProperty =
			DependencyProperty.Register(nameof(InputDeviceTypes), typeof(CoreInputDeviceTypes), typeof(InkCanvasControllerBehavior), new PropertyMetadata(
					CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Touch
				));



		public string OutputBase64String
		{
			get { return (string)GetValue(OutputBase64StringProperty); }
			set { SetValue(OutputBase64StringProperty, value); }
		}

		// Using a DependencyProperty as the backing store for OutputBase64String.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty OutputBase64StringProperty =
			DependencyProperty.Register(nameof(OutputBase64String), typeof(string), typeof(InkCanvasControllerBehavior), new PropertyMetadata(null));




		public string BackgroundImageBase64String
		{
			get { return (string)GetValue(BackgroundImageBase64StringProperty); }
			set { SetValue(BackgroundImageBase64StringProperty, value); }
		}

		// Using a DependencyProperty as the backing store for BackgroundImageBase64String.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty BackgroundImageBase64StringProperty =
			DependencyProperty.Register(nameof(BackgroundImageBase64String), typeof(string), typeof(InkCanvasControllerBehavior), new PropertyMetadata(null));



		public void RenderImageToOutput()
		{

			try
			{
				CanvasDevice device = CanvasDevice.GetSharedDevice();
				CanvasRenderTarget renderTarget = new CanvasRenderTarget(device, (int)AssociatedObject.ActualWidth, (int)AssociatedObject.ActualHeight, 96);

				using (var ds = renderTarget.CreateDrawingSession())
				{
					ds.Clear(Colors.White);
					if (BackgroundImageBase64String != null)
					{
						var b = Convert.FromBase64String(BackgroundImageBase64String);
						var stmbuffer = new InMemoryRandomAccessStream();
						stmbuffer.AsStreamForWrite().AsOutputStream().WriteAsync(b.AsBuffer()).AsTask().Wait();
						var canbit = CanvasBitmap.LoadAsync(ds, stmbuffer, 96).AsTask().Result;
						//var sfbit = SoftwareBitmap.CreateCopyFromBuffer(b.AsBuffer(), BitmapPixelFormat.Bgra8, (int)AssociatedObject.ActualWidth, (int)AssociatedObject.ActualHeight);
						//var canbit = CanvasBitmap.CreateFromSoftwareBitmap(device, sfbit);
						ds.DrawImage(canbit);
					}
					ds.DrawInk(AssociatedObject.InkPresenter.StrokeContainer.GetStrokes());
				}
				var stm = new InMemoryRandomAccessStream();
				renderTarget.SaveAsync(stm, CanvasBitmapFileFormat.Png).AsTask().Wait();
				var readfrom = stm.GetInputStreamAt(0).AsStreamForRead();
				var ms = new MemoryStream();
				readfrom.CopyTo(ms);
				OutputBase64String = Convert.ToBase64String(ms.ToArray());

				//using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
				//	await renderTarget.SaveAsync(fileStream, CanvasBitmapFileFormat.Jpeg, 1f);
				if (ImageRendered != null)
				{
					ImageRendered(this, EventArgs.Empty);
				}

			}
			catch (Exception ex)
			{							  
				throw;
			}
		}

		public event EventHandler<EventArgs> ImageRendered;

	}
}
