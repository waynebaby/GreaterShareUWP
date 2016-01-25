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
					ThumbnailStream = null,
					PackageFamilyName = "PackageFamilyName",
					Title = "Title",
					AvialableShareItems = new ObservableCollection<Models.Sharing.IShareItem>
					 {
						new TextSharedItem {  Text="okokok"},
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


		public IShareItem SelectedShareItem
		{
			get { return _SelectedShareItemLocator(this).Value; }
			set { _SelectedShareItemLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property IShareItem SelectedShareItem Setup        
		protected Property<IShareItem> _SelectedShareItem = new Property<IShareItem> { LocatorFunc = _SelectedShareItemLocator };
		static Func<BindableBase, ValueContainer<IShareItem>> _SelectedShareItemLocator = RegisterContainerLocator<IShareItem>(nameof(SelectedShareItem), model => model.Initialize(nameof(SelectedShareItem), ref model._SelectedShareItem, ref _SelectedShareItemLocator, _SelectedShareItemDefaultValueFactory));
		static Func<IShareItem> _SelectedShareItemDefaultValueFactory = () => default(IShareItem);
		#endregion



		public CommandModel<ReactiveCommand, string> CommandShareAgain
		{
			get { return _CommandShareAgainLocator(this).Value; }
			set { _CommandShareAgainLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, string> CommandShareAgain Setup        

		protected Property<CommandModel<ReactiveCommand, string>> _CommandShareAgain = new Property<CommandModel<ReactiveCommand, string>> { LocatorFunc = _CommandShareAgainLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, string>>> _CommandShareAgainLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, string>>(nameof(CommandShareAgain), model => model.Initialize(nameof(CommandShareAgain), ref model._CommandShareAgain, ref _CommandShareAgainLocator, _CommandShareAgainDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, string>> _CommandShareAgainDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandShareAgain);           // Command resource  
				var commandId = nameof(CommandShareAgain);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.DoExecuteUIBusyTask(
						vm,
						async e =>
						{
							//Todo: Add ShareAgain logic here, or
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

							  

	}

}

