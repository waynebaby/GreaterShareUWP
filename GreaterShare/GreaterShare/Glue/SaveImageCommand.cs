using GreaterShare.Models;
using Microsoft.Practices.ServiceLocation;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace GreaterShare.Glue
{
	public class SaveImageCommand : ReactiveCommand
	{
		public SaveImageCommand() : base(true)
		{

			this
				.Select(e => e.EventArgs.Parameter as MemoryStreamBase64Item)
				.Where(e => e != null)
				.Subscribe(
				 async e =>
				   {
					   try
					   {


						   var fp = new Windows.Storage.Pickers.FileSavePicker();
						   fp.FileTypeChoices.Add("Png Image files", new List<string> { ".png" });
						   fp.DefaultFileExtension = ".png";
						   var fpicked = await fp.PickSaveFileAsync();
						   if (fpicked != null)
						   {
							   using (var starget = await fpicked.OpenAsync(FileAccessMode.ReadWrite))
							   {
								   var ssouce = await e.GetRandowmAccessStreamAsync();
								   //var bi = new BitmapImage();
								   //bi.SetSource(ssouce);

								   BitmapDecoder decoder = await BitmapDecoder.CreateAsync(ssouce);

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

									
								   //var image = new WriteableBitmap(pixelData., bi.PixelHeight);

								   //image.SetSource(ssouce);

								   var BitmapEncoderGuid = BitmapEncoder.PngEncoderId;
								   BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoderGuid, starget);
								   //var buf = image.PixelBuffer;
								   //Stream pixelStream = buf.AsStream();
								   //pixelStream.Position = 0;
								   //byte[] pixels = new byte[buf.Length];
								   //buf.
								   //var dr = DataReader.FromBuffer(buf);
								  
								   //dr.ReadBytes(pixels);
								   //buf.CopyTo(0, pixels, 0, pixels.Length);
								   encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
											 (uint)decoder.PixelWidth,
											 (uint)decoder.PixelHeight,
											 96.0,
											 96.0,
											 pixelData.DetachPixelData ());
								   await encoder.FlushAsync();


								   //await ssouce.AsStreamForRead().CopyToAsync(starget.AsStreamForWrite());
							   }
							   var msb = new MessageDialog("What's next?", "File Saved.");
							   var commandOpenFile = new UICommand(
								   "Open File",
								   async c =>
								   {
									   await Launcher.LaunchFileAsync(fpicked);
								   });
							   // var commandOpenFold = new UICommand(
							   //  "Open Folder",
							   //async c =>
							   //{
							   //	await Launcher.LaunchFolderAsync(
							   //		await fpicked.GetParentAsync(),
							   //		new FolderLauncherOptions { DesiredRemainingView = Windows.UI.ViewManagement.ViewSizePreference.UseHalf });
							   //}
							   //  );
							   var commandCancelIt = new UICommand("Leave");
							   msb.Commands.Add(commandOpenFile);
							   //msb.Commands.Add(commandOpenFold);
							   msb.Commands.Add(commandCancelIt);
							   msb.CancelCommandIndex = 1;
							   msb.DefaultCommandIndex = 0;
							   var selected = await msb.ShowAsync();

						   }
					   }
					   catch (Exception ex)
					   {
					   }
				   });




		}

	}
}
