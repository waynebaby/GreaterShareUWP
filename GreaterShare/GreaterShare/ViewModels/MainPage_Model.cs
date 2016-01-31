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



	}

}

