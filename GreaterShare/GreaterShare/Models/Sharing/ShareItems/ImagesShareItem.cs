//using MVVMSidekick.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Threading.Tasks;
//using Windows.Storage;

//namespace GreaterShare.Models.Sharing.ShareItems
//{
//	[DataContract]
//	public class ImagesShareItem : BindableBase<ImagesShareItem>
//	{

//		[DataMember]

//		public List<IStorageItem> Images
//		{
//			get { return _ImagesLocator(this).Value; }
//			set { _ImagesLocator(this).SetValueAndTryNotify(value); }
//		}
//		#region Property List<IStorageItem> Images Setup        
//		protected Property<List<IStorageItem>> _Images = new Property<List<IStorageItem>> { LocatorFunc = _ImagesLocator };
//		static Func<BindableBase, ValueContainer<List<IStorageItem>>> _ImagesLocator = RegisterContainerLocator<List<IStorageItem>>("Images", model => model.Initialize("Images", ref model._Images, ref _ImagesLocator, _ImagesDefaultValueFactory));
//		static Func<List<IStorageItem>> _ImagesDefaultValueFactory = () => default(List<IStorageItem>);
//		#endregion



//	}
//}
