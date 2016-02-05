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
					Text = "hhh",
					AvialableShareItems = new ObservableCollection<object>
					 {
						new  WebLinkShareItem {  WebLink=new Uri  ("Http://notok")},
					 }
				};
			}
			else
			{
				ReceivedShareItem = new ReceivedShareItem();
			}
		}



		protected override Task OnBindedViewLoad(IView view)
		{
			//EventRouter.Instance.RaiseEvent(this, this, "Loaded");
			App.CurrentFile
				   .AsObservable()
				   //.Where(x => x != null)
				   .ObserveOn(this.Dispatcher)
				   .Subscribe(
				   async f =>
				   {
					   if (f == null)
					   {
						   ReceivedShareItem = null;
					   }
					   else
					   {
						   var loadService = ServiceLocator.Instance.Resolve<Services.ISubStorageService>();
						   var file = f as StorageFile;
						   ReceivedShareItem = await loadService.LoadFromFileAsync<ReceivedShareItem>(file);
					   }
				   })
				   .DisposeWhenUnload(this);

			return base.OnBindedViewLoad(view);
		}


		public ReceivedShareItem ClipboardImportingItem
		{
			get { return _ClipboardImportingItemLocator(this).Value; }
			set { _ClipboardImportingItemLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property ReceivedShareItem ClipboardImportingItem Setup        
		protected Property<ReceivedShareItem> _ClipboardImportingItem = new Property<ReceivedShareItem> { LocatorFunc = _ClipboardImportingItemLocator };
		static Func<BindableBase, ValueContainer<ReceivedShareItem>> _ClipboardImportingItemLocator = RegisterContainerLocator<ReceivedShareItem>(nameof(ClipboardImportingItem), model => model.Initialize(nameof(ClipboardImportingItem), ref model._ClipboardImportingItem, ref _ClipboardImportingItemLocator, _ClipboardImportingItemDefaultValueFactory));
		static Func<ReceivedShareItem> _ClipboardImportingItemDefaultValueFactory = () => default(ReceivedShareItem);
		#endregion



		public Object CurrentViewingItem
		{
			get { return _CurrentViewingItemLocator(this).Value; }
			set { _CurrentViewingItemLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Object CurrentViewingItem Setup        
		protected Property<Object> _CurrentViewingItem = new Property<Object> { LocatorFunc = _CurrentViewingItemLocator };
		static Func<BindableBase, ValueContainer<Object>> _CurrentViewingItemLocator = RegisterContainerLocator<Object>(nameof(CurrentViewingItem), model => model.Initialize(nameof(CurrentViewingItem), ref model._CurrentViewingItem, ref _CurrentViewingItemLocator, _CurrentViewingItemDefaultValueFactory));
		static Func<Object> _CurrentViewingItemDefaultValueFactory = () => default(Object);
		#endregion




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



		public int FocusingViewIndex
		{
			get { return _FocusingViewIndexLocator(this).Value; }
			set { _FocusingViewIndexLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property int FocusingViewIndex Setup        
		protected Property<int> _FocusingViewIndex = new Property<int> { LocatorFunc = _FocusingViewIndexLocator };
		static Func<BindableBase, ValueContainer<int>> _FocusingViewIndexLocator = RegisterContainerLocator<int>(nameof(FocusingViewIndex), model => model.Initialize(nameof(FocusingViewIndex), ref model._FocusingViewIndex, ref _FocusingViewIndexLocator, _FocusingViewIndexDefaultValueFactory));
		static Func<int> _FocusingViewIndexDefaultValueFactory = () => default(int);
		#endregion



		public CommandModel<ReactiveCommand, String> CommandReshareSelected
		{
			get { return _CommandReshareSelectedLocator(this).Value; }
			set { _CommandReshareSelectedLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandReshareSelected Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandReshareSelected = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandReshareSelectedLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandReshareSelectedLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandReshareSelected), model => model.Initialize(nameof(CommandReshareSelected), ref model._CommandReshareSelected, ref _CommandReshareSelectedLocator, _CommandReshareSelectedDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandReshareSelectedDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandReshareSelected);           // Command resource  
				var commandId = nameof(CommandReshareSelected);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.DoExecuteUIBusyTask(
						vm,
						async e =>
						{
							if (vm.ReceivedShareItem?.AvialableShareItems !=null)
							{
								var shareitems = vm.ReceivedShareItem.AvialableShareItems.OfType<IShareItem>()
									.Where (x=>x.IsSelected).ToArray();
								if (shareitems.Length>0)
								{

									var svc = ServiceLocator.Instance.Resolve<IShareService>();

									var nr = new ReceivedShareItem();
									vm.ReceivedShareItem.CopyTo(nr);
									nr.AvialableShareItems = new ObservableCollection<object>(shareitems);
									await svc.ShareItemAsync(nr);
								}
							}

							//Todo: Add ReshareSelected logic here, or
							await MVVMSidekick.Utilities.TaskExHelper.Yield();
						})
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				var cmdmdl = cmd.CreateCommandModel(resource);

				cmd.ListenCanExecuteObservable(
				  vm.ListenChanged(m => m.ReceivedShareItem, m => m.IsUIBusy)
				  .Select(
					  x => vm.ReceivedShareItem != null
					  && vm.ReceivedShareItem.AvialableShareItems != null
					  && !vm.IsUIBusy));
				return cmdmdl;
			};

		#endregion



		public CommandModel<ReactiveCommand, String> CommandReshareCurrent
		{
			get { return _CommandReshareCurrentLocator(this).Value; }
			set { _CommandReshareCurrentLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandReshareCurrent Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandReshareCurrent = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandReshareCurrentLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandReshareCurrentLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandReshareCurrent), model => model.Initialize(nameof(CommandReshareCurrent), ref model._CommandReshareCurrent, ref _CommandReshareCurrentLocator, _CommandReshareCurrentDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandReshareCurrentDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandReshareCurrent);           // Command resource  
				var commandId = nameof(CommandReshareCurrent);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: false ) { ViewModel = model }; //New Command Core
				
				cmd.DoExecuteUIBusyTask(
						vm,
						async e =>
						{
							var current = vm.CurrentViewingItem;
							if (current != null)
							{
								var svc = ServiceLocator.Instance.Resolve<IShareService>();

								var nr = new ReceivedShareItem();
								vm.ReceivedShareItem.CopyTo(nr);
								nr.AvialableShareItems = new ObservableCollection<object>();
								nr.AvialableShareItems.Add(current);

								await svc.ShareItemAsync(nr);
							}
							//Todo: Add ReshareCurrent logic here, or
							await MVVMSidekick.Utilities.TaskExHelper.Yield();
						})
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				var cmdmdl = cmd.CreateCommandModel(resource);
				cmd.ListenCanExecuteObservable(
				  vm.ListenChanged(m => m.CurrentViewingItem,m=>m.IsUIBusy)
				  .Select(
					  x => vm.ReceivedShareItem != null 
					  && vm.CurrentViewingItem != null
					  && !vm.IsUIBusy)
				);
				//cmdmdl.ListenToIsUIBusy(
				//	model: vm,
				//	canExecuteWhenBusy: false);
				return cmdmdl;
			};

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
				var cmd = new ReactiveCommand(canExecute: false) { ViewModel = model }; //New Command Core

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

				cmd.ListenCanExecuteObservable(
								vm.ListenChanged(x => x.ReceivedShareItem, x => vm.IsUIBusy)
								.Select(x => vm.ReceivedShareItem != null
									&& vm.ReceivedShareItem.AvialableShareItems != null
									&& vm.ReceivedShareItem.AvialableShareItems.Count > 0
									&& !vm.IsUIBusy));
				return cmdmdl;
			};

		#endregion


		public CommandModel<ReactiveCommand, String> CommandLoadFromUserFile
		{
			get { return _CommandLoadFromUserFileLocator(this).Value; }
			set { _CommandLoadFromUserFileLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandLoadFromUserFile Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandLoadFromUserFile = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLoadFromUserFileLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoadFromUserFileLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandLoadFromUserFile), model => model.Initialize(nameof(CommandLoadFromUserFile), ref model._CommandLoadFromUserFile, ref _CommandLoadFromUserFileLocator, _CommandLoadFromUserFileDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoadFromUserFileDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandLoadFromUserFile);           // Command resource  
				var commandId = nameof(CommandLoadFromUserFile);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.DoExecuteUIBusyTask(
						vm,
						async e =>
						{
							var fp = new Windows.Storage.Pickers.FileOpenPicker();
							fp.FileTypeFilter.Add(App.FileExtension);
							fp.CommitButtonText = "Load";
							var fpicked = await fp.PickSingleFileAsync();
							if (fpicked != null)
							{
								var fservice = ServiceLocator.Instance.Resolve<Services.ISubStorageService>();
								vm.ReceivedShareItem = await fservice.LoadFromFileAsync<ReceivedShareItem>(fpicked);
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


		#region Property CommandModel<ReactiveCommand, String> CommandSaveToUserFile Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandSaveToUserFile = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandSaveToUserFileLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandSaveToUserFileLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandSaveToUserFile), model => model.Initialize(nameof(CommandSaveToUserFile), ref model._CommandSaveToUserFile, ref _CommandSaveToUserFileLocator, _CommandSaveToUserFileDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandSaveToUserFileDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandSaveToUserFile);           // Command resource  
				var commandId = nameof(CommandSaveToUserFile);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: false ) { ViewModel = model }; //New Command Core


				cmd.DoExecuteUIBusyTask(
						vm,
						async e =>
						{
							var fp = new Windows.Storage.Pickers.FileSavePicker();
							fp.FileTypeChoices.Add("Greater Share Files", new List<string> { App.FileExtension });
							fp.DefaultFileExtension = App.FileExtension;
							var fpicked = await fp.PickSaveFileAsync();
							if (fpicked != null)
							{
								var fservice = ServiceLocator.Instance.Resolve<Services.ISubStorageService>();
								await fservice.SaveToFileAsync(fpicked, vm.ReceivedShareItem);
							}
						})
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				var cmdmdl = cmd.CreateCommandModel(resource);
				cmd.ListenCanExecuteObservable(
					vm.ListenChanged(x => x.ReceivedShareItem,x=>x.IsUIBusy)
					.Select(x => vm.ReceivedShareItem != null &&(!vm.IsUIBusy)));
				//cmdmdl.ListenToIsUIBusy(
				//	model: vm,
				//	canExecuteWhenBusy: false);
				return cmdmdl;
			};

		#endregion


		public CommandModel<ReactiveCommand, String> CommandGetFromClipboard
		{
			get { return _CommandGetFromClipboardLocator(this).Value; }
			set { _CommandGetFromClipboardLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandGetFromClipboard Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandGetFromClipboard = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGetFromClipboardLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGetFromClipboardLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandGetFromClipboard), model => model.Initialize(nameof(CommandGetFromClipboard), ref model._CommandGetFromClipboard, ref _CommandGetFromClipboardLocator, _CommandGetFromClipboardDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGetFromClipboardDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandGetFromClipboard);           // Command resource  
				var commandId = nameof(CommandGetFromClipboard);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.DoExecuteUIBusyTask(
						vm,
						async e =>
						{
							var svc = ServiceLocator.Instance.Resolve<IShareService>();
							var item = await svc.GetFromClipboardAsync();
							vm.ClipboardImportingItem = item;
							item.Title = "Pasted to Greater Share at " + DateTime.Now.ToString();
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





		public CommandModel<ReactiveCommand, String> CommandPushClipToCurrentItem
		{
			get { return _CommandPushClipToCurrentItemLocator(this).Value; }
			set { _CommandPushClipToCurrentItemLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandPushClipToCurrentItem Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandPushClipToCurrentItem = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandPushClipToCurrentItemLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandPushClipToCurrentItemLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandPushClipToCurrentItem), model => model.Initialize(nameof(CommandPushClipToCurrentItem), ref model._CommandPushClipToCurrentItem, ref _CommandPushClipToCurrentItemLocator, _CommandPushClipToCurrentItemDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandPushClipToCurrentItemDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandPushClipToCurrentItem);           // Command resource  
				var commandId = nameof(CommandPushClipToCurrentItem);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.DoExecuteUIBusyTask(
						vm,
						async e =>
						{
							vm.ReceivedShareItem = vm.ClipboardImportingItem;
							vm.FocusingViewIndex = 0;
							//Todo: Add PushClipToCurrentItem logic here, or
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

