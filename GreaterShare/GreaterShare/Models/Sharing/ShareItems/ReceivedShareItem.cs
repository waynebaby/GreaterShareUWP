using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace GreaterShare.Models.Sharing.ShareItems
{
	[DataContract]
	public class ReceivedShareItem : ShareItemBase<ReceivedShareItem>
	{
		[DataMember]
		public string PackageFamilyName
		{
			get { return _PackageFamilyNameLocator(this).Value; }
			set { _PackageFamilyNameLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string PackageFamilyName Setup        
		protected Property<string> _PackageFamilyName = new Property<string> { LocatorFunc = _PackageFamilyNameLocator };
		static Func<BindableBase, ValueContainer<string>> _PackageFamilyNameLocator = RegisterContainerLocator<string>(nameof(PackageFamilyName), model => model.Initialize(nameof(PackageFamilyName), ref model._PackageFamilyName, ref _PackageFamilyNameLocator, _PackageFamilyNameDefaultValueFactory));
		static Func<string> _PackageFamilyNameDefaultValueFactory = () => default(string);
		#endregion

		[DataMember]
		public Color LogoBackgroundColor
		{
			get { return _LogoBackgroundColorLocator(this).Value; }
			set { _LogoBackgroundColorLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Color LogoBackgroundColor Setup        
		protected Property<Color> _LogoBackgroundColor = new Property<Color> { LocatorFunc = _LogoBackgroundColorLocator };
		static Func<BindableBase, ValueContainer<Color>> _LogoBackgroundColorLocator = RegisterContainerLocator<Color>(nameof(LogoBackgroundColor), model => model.Initialize(nameof(LogoBackgroundColor), ref model._LogoBackgroundColor, ref _LogoBackgroundColorLocator, _LogoBackgroundColorDefaultValueFactory));
		static Func<Color> _LogoBackgroundColorDefaultValueFactory = () => default(Color);
		#endregion

		[DataMember]
		public Stream Square30x30Logo
		{
			get { return _Square30x30LogoLocator(this).Value; }
			set { _Square30x30LogoLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Stream Square30x30Logo Setup        
		protected Property<Stream> _Square30x30Logo = new Property<Stream> { LocatorFunc = _Square30x30LogoLocator };
		static Func<BindableBase, ValueContainer<Stream>> _Square30x30LogoLocator = RegisterContainerLocator<Stream>(nameof(Square30x30Logo), model => model.Initialize(nameof(Square30x30Logo), ref model._Square30x30Logo, ref _Square30x30LogoLocator, _Square30x30LogoDefaultValueFactory));
		static Func<Stream> _Square30x30LogoDefaultValueFactory = () => default(Stream);
		#endregion


		[DataMember]
		public Stream ThumbnailStream
		{
			get { return _ThumbnailStreamLocator(this).Value; }
			set { _ThumbnailStreamLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Stream ThumbnailStream Setup        
		protected Property<Stream> _ThumbnailStream = new Property<Stream> { LocatorFunc = _ThumbnailStreamLocator };
		static Func<BindableBase, ValueContainer<Stream>> _ThumbnailStreamLocator = RegisterContainerLocator<Stream>(nameof(ThumbnailStream), model => model.Initialize(nameof(ThumbnailStream), ref model._ThumbnailStream, ref _ThumbnailStreamLocator, _ThumbnailStreamDefaultValueFactory));
		static Func<Stream> _ThumbnailStreamDefaultValueFactory = () => default(Stream);
		#endregion


		[DataMember]
		public string QuickLinkId
		{
			get { return _QuickLinkIdLocator(this).Value; }
			set { _QuickLinkIdLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string QuickLinkId Setup        
		protected Property<string> _QuickLinkId = new Property<string> { LocatorFunc = _QuickLinkIdLocator };
		static Func<BindableBase, ValueContainer<string>> _QuickLinkIdLocator = RegisterContainerLocator<string>(nameof(QuickLinkId), model => model.Initialize(nameof(QuickLinkId), ref model._QuickLinkId, ref _QuickLinkIdLocator, _QuickLinkIdDefaultValueFactory));
		static Func<string> _QuickLinkIdDefaultValueFactory = () => default(string);
		#endregion




		public ObservableCollection<IShareItem> AvialableShareItems
		{
			get { return _AvialableShareItemsLocator(this).Value; }
			set { _AvialableShareItemsLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property ObservableCollection<IShareItem> AvialableShareItems Setup        
		protected Property<ObservableCollection<IShareItem>> _AvialableShareItems = new Property<ObservableCollection<IShareItem>> { LocatorFunc = _AvialableShareItemsLocator };
		static Func<BindableBase, ValueContainer<ObservableCollection<IShareItem>>> _AvialableShareItemsLocator = RegisterContainerLocator<ObservableCollection<IShareItem>>(nameof(AvialableShareItems), model => model.Initialize(nameof(AvialableShareItems), ref model._AvialableShareItems, ref _AvialableShareItemsLocator, _AvialableShareItemsDefaultValueFactory));
		static Func<ObservableCollection<IShareItem>> _AvialableShareItemsDefaultValueFactory = () => new ObservableCollection<IShareItem>();
		#endregion


	}
}
