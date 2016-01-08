using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace GreaterShare.Models.ClipboardData.ContentEntries
{
	public class BitmapEntry : ClipboardContentDataEntryBase<BitmapEntry>
	{

		public BitmapImage Image
		{
			get { return _ImageLocator(this).Value; }
			set { _ImageLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property BitmapImage Image Setup        
		protected Property<BitmapImage> _Image = new Property<BitmapImage> { LocatorFunc = _ImageLocator };
		static Func<BindableBase, ValueContainer<BitmapImage>> _ImageLocator = RegisterContainerLocator<BitmapImage>(nameof(Image), model => model.Initialize(nameof(Image), ref model._Image, ref _ImageLocator, _ImageDefaultValueFactory));
		static Func<BitmapImage> _ImageDefaultValueFactory = () => default(BitmapImage);
		#endregion


	}
}
