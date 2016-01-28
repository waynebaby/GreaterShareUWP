using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GreaterShare.Models.Sharing.ShareItems
{
	[DataContract]
	public class FilesShareItem : BindableBase<FilesShareItem>
	{

		public IReadOnlyList<IStorageItem> StorageItems
		{
			get { return _StorageItemsLocator(this).Value; }
			set { _StorageItemsLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property IReadOnlyList<IStorageItem> StorageItems Setup        
		protected Property<IReadOnlyList<IStorageItem>> _StorageItems = new Property<IReadOnlyList<IStorageItem>> { LocatorFunc = _StorageItemsLocator };
		static Func<BindableBase, ValueContainer<IReadOnlyList<IStorageItem>>> _StorageItemsLocator = RegisterContainerLocator<IReadOnlyList<IStorageItem>>(nameof(StorageItems), model => model.Initialize(nameof(StorageItems), ref model._StorageItems, ref _StorageItemsLocator, _StorageItemsDefaultValueFactory));
		static Func<IReadOnlyList<IStorageItem>> _StorageItemsDefaultValueFactory = () => default(IReadOnlyList<IStorageItem>);
		#endregion

	}
}
