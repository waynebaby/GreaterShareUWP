using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GreaterShare.Models.Sharing.ShareItems
{
	[DataContract]
	public class FilesShareItem : BindableBase<FilesShareItem>, IShareItem
	{
		public FilesShareItem()
		{
			WireEvent();
		}

		protected override void OnDeserializingActions()
		{
			base.OnDeserializingActions();
			WireEvent();
		}
		//[DataMember]
		//public IReadOnlyList<IStorageItem> StorageItems
		//{
		//	get { return _StorageItemsLocator(this).Value; }
		//	set { _StorageItemsLocator(this).SetValueAndTryNotify(value); }
		//}
		//#region Property IReadOnlyList<IStorageItem> StorageItems Setup        
		//protected Property<IReadOnlyList<IStorageItem>> _StorageItems = new Property<IReadOnlyList<IStorageItem>> { LocatorFunc = _StorageItemsLocator };
		//static Func<BindableBase, ValueContainer<IReadOnlyList<IStorageItem>>> _StorageItemsLocator = RegisterContainerLocator<IReadOnlyList<IStorageItem>>(nameof(StorageItems), model => model.Initialize(nameof(StorageItems), ref model._StorageItems, ref _StorageItemsLocator, _StorageItemsDefaultValueFactory));
		//static Func<IReadOnlyList<IStorageItem>> _StorageItemsDefaultValueFactory = () => default(IReadOnlyList<IStorageItem>);
		//#endregion

		[DataMember]
		public ObservableCollection<FileItem> StorageFiles
		{
			get { return _StorageFilesLocator(this).Value; }
			set { _StorageFilesLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property ObservableCollection<FileItem> StorageFiles Setup        
		protected Property<ObservableCollection<FileItem>> _StorageFiles = new Property<ObservableCollection<FileItem>> { LocatorFunc = _StorageFilesLocator };
		static Func<BindableBase, ValueContainer<ObservableCollection<FileItem>>> _StorageFilesLocator = RegisterContainerLocator<ObservableCollection<FileItem>>(nameof(StorageFiles), model => model.Initialize(nameof(StorageFiles), ref model._StorageFiles, ref _StorageFilesLocator, _StorageFilesDefaultValueFactory));
		static Func<ObservableCollection<FileItem>> _StorageFilesDefaultValueFactory = () => default(ObservableCollection<FileItem>);
		#endregion

		public bool IsSelected
		{
			get { return _IsSelectedLocator(this).Value; }
			set { _IsSelectedLocator(this).SetValueAndTryNotify(value); }
		}

	
		#region Property bool IsSelected Setup        
		protected Property<bool> _IsSelected = new Property<bool> { LocatorFunc = _IsSelectedLocator };
		static Func<BindableBase, ValueContainer<bool>> _IsSelectedLocator = RegisterContainerLocator<bool>(nameof(IsSelected), model => model.Initialize(nameof(IsSelected), ref model._IsSelected, ref _IsSelectedLocator, _IsSelectedDefaultValueFactory));
		static Func<bool> _IsSelectedDefaultValueFactory = () => true;


		#endregion

		public void WireEvent()
		{
			if (!IsEventWired)
			{
				IsEventWired = true;
			}
		}

		public bool IsEventWired { get; set; } = false;

	}


	[DataContract()] //if you want
	public class FileItem : BindableBase<FileItem>
	{

		[DataMember] 
		public string ContentType
		{
			get { return _ContentTypeLocator(this).Value; }
			set { _ContentTypeLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string ContentType Setup        
		protected Property<string> _ContentType = new Property<string> { LocatorFunc = _ContentTypeLocator };
		static Func<BindableBase, ValueContainer<string>> _ContentTypeLocator = RegisterContainerLocator<string>(nameof(ContentType), model => model.Initialize(nameof(ContentType), ref model._ContentType, ref _ContentTypeLocator, _ContentTypeDefaultValueFactory));
		static Func<string> _ContentTypeDefaultValueFactory = () => default(string);
		#endregion

		[DataMember]	  
		public string FileName
		{
			get { return _FileNameLocator(this).Value; }
			set { _FileNameLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string FileName Setup        
		protected Property<string> _FileName = new Property<string> { LocatorFunc = _FileNameLocator };
		static Func<BindableBase, ValueContainer<string>> _FileNameLocator = RegisterContainerLocator<string>(nameof(FileName), model => model.Initialize(nameof(FileName), ref model._FileName, ref _FileNameLocator, _FileNameDefaultValueFactory));
		static Func<string> _FileNameDefaultValueFactory = () => default(string);
		#endregion



		[DataMember]
		public string AccessToken
		{
			get { return _AccessTokenLocator(this).Value; }
			set { _AccessTokenLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string AccessToken Setup        
		protected Property<string> _AccessToken = new Property<string> { LocatorFunc = _AccessTokenLocator };
		static Func<BindableBase, ValueContainer<string>> _AccessTokenLocator = RegisterContainerLocator<string>(nameof(AccessToken), model => model.Initialize(nameof(AccessToken), ref model._AccessToken, ref _AccessTokenLocator, _AccessTokenDefaultValueFactory));
		static Func<string> _AccessTokenDefaultValueFactory = () => default(string);
		#endregion

		[DataMember]
		public string PossiblePath
		{
			get { return _PossiblePathLocator(this).Value; }
			set { _PossiblePathLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string PossiblePath Setup        
		protected Property<string> _PossiblePath = new Property<string> { LocatorFunc = _PossiblePathLocator };
		static Func<BindableBase, ValueContainer<string>> _PossiblePathLocator = RegisterContainerLocator<string>(nameof(PossiblePath), model => model.Initialize(nameof(PossiblePath), ref model._PossiblePath, ref _PossiblePathLocator, _PossiblePathDefaultValueFactory));
		static Func<string> _PossiblePathDefaultValueFactory = () => default(string);
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


		public bool IsTokenAvaliableNow
		{
			get { return _IsTokenAvaliableNowLocator(this).Value; }
			set { _IsTokenAvaliableNowLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property bool IsTokenAvaliableNow Setup        
		protected Property<bool> _IsTokenAvaliableNow = new Property<bool> { LocatorFunc = _IsTokenAvaliableNowLocator };
		static Func<BindableBase, ValueContainer<bool>> _IsTokenAvaliableNowLocator = RegisterContainerLocator<bool>(nameof(IsTokenAvaliableNow), model => model.Initialize(nameof(IsTokenAvaliableNow), ref model._IsTokenAvaliableNow, ref _IsTokenAvaliableNowLocator, _IsTokenAvaliableNowDefaultValueFactory));
		static Func<bool> _IsTokenAvaliableNowDefaultValueFactory = () => default(bool);
		#endregion

	}





}
