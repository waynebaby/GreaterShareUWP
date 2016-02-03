using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using GreaterShare.Models.Sharing.ShareItems;
using Windows.UI;
using Windows.Storage;
using GreaterShare.Services;
using MVVMSidekick.EventRouting;

namespace GreaterShare.ViewModels
{

	[DataContract]
	public class MainPage_Model : ViewModelBase<MainPage_Model>
	{
		public MainPage_Model()
		{
			if (IsInDesignMode)
			{
				ReceivedShareItem = new ReceivedShareItem
				{
					ContentSourceApplicationLink = new Uri("http://ContentSourceApplicationLink/"),
					ContentSourceWebLink = new Uri("http://ContentSourceWebLink"),
					DefaultFailedDisplayText = "Default failed",
					Description = "Description",
					LogoBackgroundColor = Colors.Red,
					QuickLinkId = "QuickLinkId",
					Square30x30Logo = null,
					Thumbnail = null,
					PackageFamilyName = "PackageFamilyName",
					Title = "Title",
					AvialableShareItems = new ObservableCollection<object>
					 {
						new TextSharedItem {  Text="okokok"},
						new  WebLinkShareItem {  WebLink=new Uri  ("Http://notok")},
					 }
				};
			}

			else

			{
				App.CurrentFile
					.AsObservable()
					.Where(x => x != null)
					.ObserveOn(this.Dispatcher)
					.Subscribe(
					async f =>
					{		 
						var loadService = ServiceLocator.Instance.Resolve<Services.ISubStorageService>();
						var file = f as StorageFile;
						ReceivedShareItem = await loadService.LoadFromFileAsync<ReceivedShareItem>(file);

					})
					.DisposeWith(this);

			}

		}



		protected override Task OnBindedViewLoad(IView view)
		{
			EventRouter.Instance.RaiseEvent(this, this, "Loaded");
			return base.OnBindedViewLoad(view);
		}


		public ReceivedShareItem ReceivedShareItem
		{
			get { return _ReceivedShareItemLocator(this).Value; }
			set { _ReceivedShareItemLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property ReceivedShareItem ReceivedShareItem Setup        
		protected Property<ReceivedShareItem> _ReceivedShareItem = new Property<ReceivedShareItem> { LocatorFunc = _ReceivedShareItemLocator };
		static Func<BindableBase, ValueContainer<ReceivedShareItem>> _ReceivedShareItemLocator = RegisterContainerLocator<ReceivedShareItem>(nameof(ReceivedShareItem), model => model.Initialize(nameof(ReceivedShareItem), ref model._ReceivedShareItem, ref _ReceivedShareItemLocator, _ReceivedShareItemDefaultValueFactory));
		static Func<ReceivedShareItem> _ReceivedShareItemDefaultValueFactory = () => default(ReceivedShareItem);
		#endregion


		public CommandModel<ReactiveCommand, String> CommandReshare
		{
			get { return _CommandReshareLocator(this).Value; }
			set { _CommandReshareLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandReshare Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandReshare = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandReshareLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandReshareLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandReshare), model => model.Initialize(nameof(CommandReshare), ref model._CommandReshare, ref _CommandReshareLocator, _CommandReshareDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandReshareDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandReshare);           // Command resource  
				var commandId = nameof(CommandReshare);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.DoExecuteUIBusyTask(
						vm,
						async e =>
						{
							try
							{

								var svc = ServiceLocator.Instance.Resolve<IShareService>();
								await svc.ShareItemAsync(vm.ReceivedShareItem);
								//Todo: Add Reshare logic here, or
								await MVVMSidekick.Utilities.TaskExHelper.Yield();

							}
							catch (Exception ex)
							{
								throw;
							}
						})
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				var cmdmdl = cmd.CreateCommandModel(resource);

				cmdmdl.ListenToIsUIBusy(
					model: vm,
					canExecuteWhenBusy: false);
				return cmdmdl;
			};

		#endregion



		public CommandModel<ReactiveCommand, String> CommandSaveToUserFile
		{
			get { return _CommandSaveToUserFileLocator(this).Value; }
			set { _CommandSaveToUserFileLocator(this).SetValueAndTryNotify(value); }
		}

		public static Func<BindableBase, CommandModel<ReactiveCommand, string>> CommandSaveToUserFileDefaultValueFactory
		{
			get
			{
				return _CommandSaveToUserFileDefaultValueFactory;
			}

			set
			{
				_CommandSaveToUserFileDefaultValueFactory = value;
			}
		}
		#region Property CommandModel<ReactiveCommand, String> CommandSaveToUserFile Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandSaveToUserFile = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandSaveToUserFileLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandSaveToUserFileLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandSaveToUserFile), model => model.Initialize(nameof(CommandSaveToUserFile), ref model._CommandSaveToUserFile, ref _CommandSaveToUserFileLocator, CommandSaveToUserFileDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandSaveToUserFileDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandSaveToUserFile);           // Command resource  
				var commandId = nameof(CommandSaveToUserFile);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.DoExecuteUIBusyTask(
						vm,
						async e =>
						{
							var fp = new Windows.Storage.Pickers.FileSavePicker();
							fp.FileTypeChoices.Add("Greater Share Files", new List<string> { App.FileExtension });
							fp.DefaultFileExtension = App.FileExtension;
							var fpicked = await fp.PickSaveFileAsync();
							//if (fpicked != null)
							//{
							//App.CurrentFile.OnNext(fpicked);
							//}			  
							//Todo: Add SaveToUserFile logic here, or

							var fservice = ServiceLocator.Instance.Resolve<Services.ISubStorageService>();
							await fservice.SaveToFileAsync(fpicked, vm.ReceivedShareItem);

							await MVVMSidekick.Utilities.TaskExHelper.Yield();
						})
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				var cmdmdl = cmd.CreateCommandModel(resource);

				cmdmdl.ListenToIsUIBusy(
					model: vm,
					canExecuteWhenBusy: false);
				return cmdmdl;
			};

		#endregion




	}

}

