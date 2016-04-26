using GreaterShare.Models;
using GreaterShare.Services;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
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

        static ResourceLoader rl = ResourceLoader.GetForViewIndependentUse();

        static string SaveImageCommandMessageDialogFileSavedNextContent { get { return rl.GetString(nameof(SaveImageCommandMessageDialogFileSavedNextContent)); } }
        static string SaveImageCommandMessageDialogMessageDialogFileSavedNextTitle { get { return rl.GetString(nameof(SaveImageCommandMessageDialogMessageDialogFileSavedNextTitle)); } }
        static string SaveImageCommandMessageDialogUICommandOpenFile { get { return rl.GetString(nameof(SaveImageCommandMessageDialogUICommandOpenFile)); } }
        static string SaveImageCommandMessageDialogUICommandLeave { get { return rl.GetString(nameof(SaveImageCommandMessageDialogUICommandLeave)); } }
        static string SaveImageCommandFileSavePickerPngFiles { get { return rl.GetString(nameof(SaveImageCommandFileSavePickerPngFiles)); } }


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
                           fp.FileTypeChoices.Add(SaveImageCommandFileSavePickerPngFiles, new List<string> { ".png" });
                           fp.DefaultFileExtension = ".png";
                           var fpicked = await fp.PickSaveFileAsync();
                           if (fpicked != null)
                           {
                               using (var saveTargetStream = await fpicked.OpenAsync(FileAccessMode.ReadWrite))
                               {
                                   var bitmapSourceStream = await e.GetRandowmAccessStreamAsync();

                                   await ServiceLocator
                                            .Instance
                                            .Resolve<IImageConvertService>()
                                            .ConverterBitmapToTargetStreamAsync(bitmapSourceStream, saveTargetStream);

                               }
                               var msb = new MessageDialog(SaveImageCommandMessageDialogFileSavedNextContent, SaveImageCommandMessageDialogMessageDialogFileSavedNextTitle);
                               var commandOpenFile = new UICommand(
                                 SaveImageCommandMessageDialogUICommandOpenFile,
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
                               var commandCancelIt = new UICommand(SaveImageCommandMessageDialogUICommandLeave);
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
