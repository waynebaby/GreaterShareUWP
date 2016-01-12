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
	public class DelayRenderedImageShareItem : ShareItemBase<DelayRenderedImageShareItem>
	{

		public Stream SelectedImage
		{
			get { return _SelectedImageLocator(this).Value; }
			set { _SelectedImageLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Stream SelectedImage Setup        
		protected Property<Stream> _SelectedImage = new Property<Stream> { LocatorFunc = _SelectedImageLocator };
		static Func<BindableBase, ValueContainer<Stream>> _SelectedImageLocator = RegisterContainerLocator<Stream>(nameof(SelectedImage), model => model.Initialize(nameof(SelectedImage), ref model._SelectedImage, ref _SelectedImageLocator, _SelectedImageDefaultValueFactory));
		static Func<Stream> _SelectedImageDefaultValueFactory = () => default(Stream);
		#endregion


	}
}
