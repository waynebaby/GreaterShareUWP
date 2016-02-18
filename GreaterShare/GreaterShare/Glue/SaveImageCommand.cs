using GreaterShare.Models;
using Microsoft.Practices.ServiceLocation;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.UI.Popups;

namespace GreaterShare.Glue
{
	public class SaveImageCommand : ReactiveCommand
	{
		public SaveImageCommand():base(true)
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
								   await ssouce.AsStreamForRead().CopyToAsync(starget.AsStreamForWrite());
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
					   catch (Exception)
					   {
					   }
				   });
		



		}

	}
}
