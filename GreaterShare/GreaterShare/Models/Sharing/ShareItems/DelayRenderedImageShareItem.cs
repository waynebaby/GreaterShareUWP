using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.Models.Sharing.ShareItems
{
	[DataContract]
	public class DelayRenderedImageShareItem : BindableBase<DelayRenderedImageShareItem>, IShareItem
	{
		//[DataMember]
		//public MemoryStream SelectedImage
		//{
		//	get { return _SelectedImageLocator(this).Value; }
		//	set { _SelectedImageLocator(this).SetValueAndTryNotify(value); }
		//}
		//#region Property MemoryStream SelectedImage Setup        
		//protected Property<MemoryStream> _SelectedImage = new Property<MemoryStream> { LocatorFunc = _SelectedImageLocator };
		//static Func<BindableBase, ValueContainer<MemoryStream>> _SelectedImageLocator = RegisterContainerLocator<MemoryStream>("SelectedImage", model => model.Initialize("SelectedImage", ref model._SelectedImage, ref _SelectedImageLocator, _SelectedImageDefaultValueFactory));
		//static Func<MemoryStream> _SelectedImageDefaultValueFactory = () => default(MemoryStream);
		//#endregion

		[DataMember]
		public MemoryStreamBase64Item SelectedImage
		{
			get { return _SelectedImageLocator(this).Value; }
			set { _SelectedImageLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property MemoryStreamBase64Item SelectedImage Setup        
		protected Property<MemoryStreamBase64Item> _SelectedImage = new Property<MemoryStreamBase64Item> { LocatorFunc = _SelectedImageLocator };
		static Func<BindableBase, ValueContainer<MemoryStreamBase64Item>> _SelectedImageLocator = RegisterContainerLocator<MemoryStreamBase64Item>(nameof(SelectedImage), model => model.Initialize(nameof(SelectedImage), ref model._SelectedImage, ref _SelectedImageLocator, _SelectedImageDefaultValueFactory));
		static Func<MemoryStreamBase64Item> _SelectedImageDefaultValueFactory = () => default(MemoryStreamBase64Item);
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


		
	}
}
