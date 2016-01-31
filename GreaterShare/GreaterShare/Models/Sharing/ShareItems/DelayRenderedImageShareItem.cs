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
	public class DelayRenderedImageShareItem : BindableBase<DelayRenderedImageShareItem>
	{
		[DataMember]
		public MemoryStream SelectedImage
		{
			get { return _SelectedImageLocator(this).Value; }
			set { _SelectedImageLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property MemoryStream SelectedImage Setup        
		protected Property<MemoryStream> _SelectedImage = new Property<MemoryStream> { LocatorFunc = _SelectedImageLocator };
		static Func<BindableBase, ValueContainer<MemoryStream>> _SelectedImageLocator = RegisterContainerLocator<MemoryStream>("SelectedImage", model => model.Initialize("SelectedImage", ref model._SelectedImage, ref _SelectedImageLocator, _SelectedImageDefaultValueFactory));
		static Func<MemoryStream> _SelectedImageDefaultValueFactory = () => default(MemoryStream);
		#endregion



	}
}
