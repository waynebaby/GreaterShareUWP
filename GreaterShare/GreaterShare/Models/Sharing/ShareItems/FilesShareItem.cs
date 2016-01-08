using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GreaterShare.Models.Sharing.ShareItems
{
	public class FilesShareItem : ShareItemBase<FilesShareItem>
	{

		public IReadOnlyList<StorageFile> StorageItems
		{
			get { return _StorageItemsLocator(this).Value; }
			set { _StorageItemsLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property IReadOnlyList<StorageFile> StorageItems Setup        
		protected Property<IReadOnlyList<StorageFile>> _StorageItems = new Property<IReadOnlyList<StorageFile>> { LocatorFunc = _StorageItemsLocator };
		static Func<BindableBase, ValueContainer<IReadOnlyList<StorageFile>>> _StorageItemsLocator = RegisterContainerLocator<IReadOnlyList<StorageFile>>(nameof(StorageItems), model => model.Initialize(nameof(StorageItems), ref model._StorageItems, ref _StorageItemsLocator, _StorageItemsDefaultValueFactory));
		static Func<IReadOnlyList<StorageFile>> _StorageItemsDefaultValueFactory = () => default(IReadOnlyList<StorageFile>);
		#endregion

	}
}
