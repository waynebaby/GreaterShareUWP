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
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using GreaterShare.Services;
using Windows.UI;
using GreaterShare.Models.Sharing;
using Windows.System;

namespace GreaterShare.ViewModels
{

	[DataContract]
	public class SharePanelPage_Model : ViewModelBase<SharePanelPage_Model>
	{
		// If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
		// 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性


		public SharePanelPage_Model()
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
		}



		public ReceivedShareItem ReceivedShareItem
		{
			get { return _ReceivedShareItemLocator(this).Value; }
			set { _ReceivedShareItemLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property ReceivedShareItem ReceivedShareItem Setup        
		protected Property<ReceivedShareItem> _ReceivedShareItem = new Property<ReceivedShareItem> { LocatorFunc = _ReceivedShareItemLocator };
		static Func<BindableBase, ValueContainer<ReceivedShareItem>> _ReceivedShareItemLocator = RegisterContainerLocator<ReceivedShareItem>("ReceivedShareItem", model => model.Initialize("ReceivedShareItem", ref model._ReceivedShareItem, ref _ReceivedShareItemLocator, _ReceivedShareItemDefaultValueFactory));
		static Func<ReceivedShareItem> _ReceivedShareItemDefaultValueFactory = () => default(ReceivedShareItem);
		#endregion



		public Object SelectedShareItem
		{
			get { return _SelectedShareItemLocator(this).Value; }
			set { _SelectedShareItemLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Object SelectedShareItem Setup        
		protected Property<Object> _SelectedShareItem = new Property<Object> { LocatorFunc = _SelectedShareItemLocator };
		static Func<BindableBase, ValueContainer<Object>> _SelectedShareItemLocator = RegisterContainerLocator<Object>(nameof(SelectedShareItem), model => model.Initialize(nameof(SelectedShareItem), ref model._SelectedShareItem, ref _SelectedShareItemLocator, _SelectedShareItemDefaultValueFactory));
		static Func<Object> _SelectedShareItemDefaultValueFactory = () => default(Object);
		#endregion




		public ShareOperation SharedOperation
		{
			get { return _SharedOperationLocator(this).Value; }
			set { _SharedOperationLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property ShareOperation SharedOperation Setup        
		protected Property<ShareOperation> _SharedOperation = new Property<ShareOperation> { LocatorFunc = _SharedOperationLocator };
		static Func<BindableBase, ValueContainer<ShareOperation>> _SharedOperationLocator = RegisterContainerLocator<ShareOperation>("SharedOperation", model => model.Initialize("SharedOperation", ref model._SharedOperation, ref _SharedOperationLocator, _SharedOperationDefaultValueFactory));
		static Func<ShareOperation> _SharedOperationDefaultValueFactory = () => default(ShareOperation);
		#endregion

		protected override async Task OnBindedViewLoad(IView view)
		{

			if (!IsInDesignMode)
			{
				if (ReceivedShareItem == null)
				{

					var shareService = ServiceLocator.Instance.Resolve<IShareService>();
					this.ReceivedShareItem = await shareService.GetReceivedSharedItemAsync(SharedOperation);

				}
			}
		}



		public CommandModel<ReactiveCommand, String> CommandSaveAndLauchMainApp
		{
			get { return _CommandSaveAndLauchMainAppLocator(this).Value; }
			set { _CommandSaveAndLauchMainAppLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandSaveAndLauchMainApp Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandSaveAndLauchMainApp = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandSaveAndLauchMainAppLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandSaveAndLauchMainAppLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandSaveAndLauchMainApp), model => model.Initialize(nameof(CommandSaveAndLauchMainApp), ref model._CommandSaveAndLauchMainApp, ref _CommandSaveAndLauchMainAppLocator, _CommandSaveAndLauchMainAppDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandSaveAndLauchMainAppDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandSaveAndLauchMainApp);           // Command resource  
				var commandId = nameof(CommandSaveAndLauchMainApp);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.DoExecuteUIBusyTask(
						vm,
						async e =>
						{
							var folderService = ServiceLocator.Instance.Resolve<ISubStorageService>();

							var name = folderService.GetNewFileName();

							try
							{
								var file = await folderService.SaveToFileAsync(name, vm.ReceivedShareItem);
								await Launcher.LaunchFileAsync(file);
							}
							catch (Exception ex)
							{

								throw;
							}
							vm.SharedOperation.ReportCompleted();
							vm.SharedOperation.DismissUI();

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

