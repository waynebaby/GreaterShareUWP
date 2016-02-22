using MVVMSidekick.ViewModels;
using MVVMSidekick.Reactive;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using System.Reactive;
using System.Reactive.Linq;

namespace GreaterShare.Models.Sharing.ShareItems
{
	[DataContract()]
	[KnownType(typeof(HtmlShareItem))]
	[KnownType(typeof(ApplicationLinkShareItem))]
	[KnownType(typeof(DelayRenderedImageShareItem))]
	//[KnownType(typeof(ErrorMessageShareItem))]
	[KnownType(typeof(FilesShareItem))]
	[KnownType(typeof(HtmlShareItem))]
	//[KnownType(typeof(ImagesShareItem))]
	[KnownType(typeof(TextShareItem))]
	[KnownType(typeof(WebLinkShareItem))]



	public class ReceivedShareItem : BindableBase<ReceivedShareItem>
	{


	
		[DataMember]
		public String Text
		{
			get { return _TextLocator(this).Value; }
			set { _TextLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property String Text Setup        
		protected Property<String> _Text = new Property<String> { LocatorFunc = _TextLocator };
		static Func<BindableBase, ValueContainer<String>> _TextLocator = RegisterContainerLocator<String>(nameof(Text), model => model.Initialize(nameof(Text), ref model._Text, ref _TextLocator, _TextDefaultValueFactory));
		static Func<String> _TextDefaultValueFactory = () => default(String);
		#endregion

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

		//[DataMember]

		//public string Square30x30LogoBase64
		//{
		//	get { return _Square30x30LogoBase64Locator(this).Value; }
		//	set { _Square30x30LogoBase64Locator(this).SetValueAndTryNotify(value); }
		//}
		//#region Property string Square30x30LogoBase64 Setup        
		//protected Property<string> _Square30x30LogoBase64 = new Property<string> { LocatorFunc = _Square30x30LogoBase64Locator };
		//static Func<BindableBase, ValueContainer<string>> _Square30x30LogoBase64Locator = RegisterContainerLocator<string>(nameof(Square30x30LogoBase64), model => model.Initialize(nameof(Square30x30LogoBase64), ref model._Square30x30LogoBase64, ref _Square30x30LogoBase64Locator, _Square30x30LogoBase64DefaultValueFactory));
		//static Func<string> _Square30x30LogoBase64DefaultValueFactory = () => default(string);
		//#endregion

		public bool IsInEditing
		{
			get { return _IsInEditingLocator(this).Value; }
			set { _IsInEditingLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property bool IsInEditing Setup        
		protected Property<bool> _IsInEditing = new Property<bool> { LocatorFunc = _IsInEditingLocator };
		static Func<BindableBase, ValueContainer<bool>> _IsInEditingLocator = RegisterContainerLocator<bool>(nameof(IsInEditing), model => model.Initialize(nameof(IsInEditing), ref model._IsInEditing, ref _IsInEditingLocator, _IsInEditingDefaultValueFactory));
		static Func<bool> _IsInEditingDefaultValueFactory = () => default(bool);
		#endregion


		[DataMember]

		public MemoryStreamBase64Item Square30x30Logo
		{
			get { return _Square30x30LogoLocator(this).Value; }
			set { _Square30x30LogoLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property MemoryStreamBase64Item Square30x30Logo Setup        
		protected Property<MemoryStreamBase64Item> _Square30x30Logo = new Property<MemoryStreamBase64Item> { LocatorFunc = _Square30x30LogoLocator };
		static Func<BindableBase, ValueContainer<MemoryStreamBase64Item>> _Square30x30LogoLocator = RegisterContainerLocator<MemoryStreamBase64Item>(nameof(Square30x30Logo), model => model.Initialize(nameof(Square30x30Logo), ref model._Square30x30Logo, ref _Square30x30LogoLocator, _Square30x30LogoDefaultValueFactory));
		static Func<MemoryStreamBase64Item> _Square30x30LogoDefaultValueFactory = () => default(MemoryStreamBase64Item);
		#endregion


		[DataMember]


		public MemoryStreamBase64Item Thumbnail
		{
			get { return _ThumbnailLocator(this).Value; }
			set { _ThumbnailLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property MemoryStreamBase64Item Thumbnail Setup        
		protected Property<MemoryStreamBase64Item> _Thumbnail = new Property<MemoryStreamBase64Item> { LocatorFunc = _ThumbnailLocator };
		static Func<BindableBase, ValueContainer<MemoryStreamBase64Item>> _ThumbnailLocator = RegisterContainerLocator<MemoryStreamBase64Item>(nameof(Thumbnail), model => model.Initialize(nameof(Thumbnail), ref model._Thumbnail, ref _ThumbnailLocator, _ThumbnailDefaultValueFactory));
		static Func<MemoryStreamBase64Item> _ThumbnailDefaultValueFactory = () => default(MemoryStreamBase64Item);
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



		[DataMember]

		public ObservableCollection<object> AvialableShareItems
		{
			get { return _AvialableShareItemsLocator(this).Value; }
			set { _AvialableShareItemsLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property ObservableCollection<object> AvialableShareItems Setup        
		protected Property<ObservableCollection<object>> _AvialableShareItems = new Property<ObservableCollection<object>> { LocatorFunc = _AvialableShareItemsLocator };
		static Func<BindableBase, ValueContainer<ObservableCollection<object>>> _AvialableShareItemsLocator = RegisterContainerLocator<ObservableCollection<object>>("AvialableShareItems", model => model.Initialize("AvialableShareItems", ref model._AvialableShareItems, ref _AvialableShareItemsLocator, _AvialableShareItemsDefaultValueFactory));
		static Func<ObservableCollection<object>> _AvialableShareItemsDefaultValueFactory = () => new ObservableCollection<object>();
		#endregion


		[DataMember]
		public string Title
		{
			get { return _TitleLocator(this).Value; }
			set { _TitleLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string Title Setup        
		protected Property<string> _Title = new Property<string> { LocatorFunc = _TitleLocator };
		static Func<BindableBase, ValueContainer<string>> _TitleLocator = RegisterContainerLocator<string>(nameof(Title), model => model.Initialize(nameof(Title), ref model._Title, ref _TitleLocator, _TitleDefaultValueFactory));
		static Func<string> _TitleDefaultValueFactory = () => default(string);
		#endregion



		[DataMember]
		public string Description
		{
			get { return _DescriptionLocator(this).Value; }
			set { _DescriptionLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string Description Setup        
		protected Property<string> _Description = new Property<string> { LocatorFunc = _DescriptionLocator };
		static Func<BindableBase, ValueContainer<string>> _DescriptionLocator = RegisterContainerLocator<string>(nameof(Description), model => model.Initialize(nameof(Description), ref model._Description, ref _DescriptionLocator, _DescriptionDefaultValueFactory));
		static Func<string> _DescriptionDefaultValueFactory = () => default(string);
		#endregion



		/// <summary>
		/// Most of Apps won't care this
		/// </summary>
		[DataMember]

		public Uri ContentSourceApplicationLink
		{
			get { return _ContentSourceApplicationLinkLocator(this).Value; }
			set { _ContentSourceApplicationLinkLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Uri ContentSourceApplicationLink Setup        
		protected Property<Uri> _ContentSourceApplicationLink = new Property<Uri> { LocatorFunc = _ContentSourceApplicationLinkLocator };
		static Func<BindableBase, ValueContainer<Uri>> _ContentSourceApplicationLinkLocator = RegisterContainerLocator<Uri>(nameof(ContentSourceApplicationLink), model => model.Initialize(nameof(ContentSourceApplicationLink), ref model._ContentSourceApplicationLink, ref _ContentSourceApplicationLinkLocator, _ContentSourceApplicationLinkDefaultValueFactory));
		static Func<Uri> _ContentSourceApplicationLinkDefaultValueFactory = () => default(Uri);
		#endregion

		[DataMember]
		public Uri ContentSourceWebLink
		{
			get { return _ContentSourceWebLinkLocator(this).Value; }
			set { _ContentSourceWebLinkLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Uri ContentSourceWebLink Setup        
		protected Property<Uri> _ContentSourceWebLink = new Property<Uri> { LocatorFunc = _ContentSourceWebLinkLocator };
		static Func<BindableBase, ValueContainer<Uri>> _ContentSourceWebLinkLocator = RegisterContainerLocator<Uri>(nameof(ContentSourceWebLink), model => model.Initialize(nameof(ContentSourceWebLink), ref model._ContentSourceWebLink, ref _ContentSourceWebLinkLocator, _ContentSourceWebLinkDefaultValueFactory));
		static Func<Uri> _ContentSourceWebLinkDefaultValueFactory = () => default(Uri);
		#endregion


		/// <summary>
		/// Most of Apps won't care this
		/// </summary>		   
		[DataMember]
		public string DefaultFailedDisplayText
		{
			get { return _DefaultFailedDisplayTextLocator(this).Value; }
			set { _DefaultFailedDisplayTextLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string DefaultFailedDisplayText Setup        
		protected Property<string> _DefaultFailedDisplayText = new Property<string> { LocatorFunc = _DefaultFailedDisplayTextLocator };
		static Func<BindableBase, ValueContainer<string>> _DefaultFailedDisplayTextLocator = RegisterContainerLocator<string>(nameof(DefaultFailedDisplayText), model => model.Initialize(nameof(DefaultFailedDisplayText), ref model._DefaultFailedDisplayText, ref _DefaultFailedDisplayTextLocator, _DefaultFailedDisplayTextDefaultValueFactory));
		static Func<string> _DefaultFailedDisplayTextDefaultValueFactory = () => default(string);
		#endregion

		static string[][] templates =
			new[]{
				new[] {
					"{2}\r\n\r\n{1}",
					"{2}\r\n\r\n/*Merged at {0:yy-MM-dd HH:mm:ss}*/\r\n{1}"},
				new [] {
					"\r\n{1}\r\n\r\n{2}",
					"/*Merged at {0:yy-MM-dd HH:mm:ss}*/\r\n{1}\r\n\r\n{2}"
				},

			};
		public static void MergeNewText(TextShareItem oldText, string newTextString, bool showTag = false, bool headInsert = false)
		{

			var template = templates[headInsert ? 1 : 0][showTag ? 1 : 0];
			if (headInsert)
			{
				oldText.Text = string.Format(template,
				  DateTime.Now,
				  newTextString,
				  oldText.Text);
			}
			else
			{
				oldText.Text = string.Format(template,
				DateTime.Now,
				newTextString,
				oldText.Text);
			}
		}


	

	}
}
