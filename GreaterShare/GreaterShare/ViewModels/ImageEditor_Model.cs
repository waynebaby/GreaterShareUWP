using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Windows.UI.Xaml.Controls;
using Windows.UI;

namespace GreaterShare.ViewModels
{

	[DataContract]
	public class ImageEditor_Model : ViewModelBase<ImageEditor_Model>
	{
		// If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
		// 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性




		protected override async Task OnBindedViewLoad(IView view)
		{



			await base.OnBindedViewLoad(view);

			this.ListenChanged(x => x.ImageResult)
				.Where(x => this.ImageResult != null)
				.Subscribe(x =>
				{
					CurrentImage.Base64String = ImageResult;
					CloseViewAndDispose();
				})
				 .DisposeWith(this);

		}

		public Models.MemoryStreamBase64Item CurrentImage
		{
			get { return _CurrentImageLocator(this).Value; }
			set { _CurrentImageLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Models.MemoryStreamBase64Item CurrentImage Setup        
		protected Property<Models.MemoryStreamBase64Item> _CurrentImage = new Property<Models.MemoryStreamBase64Item> { LocatorFunc = _CurrentImageLocator };
		static Func<BindableBase, ValueContainer<Models.MemoryStreamBase64Item>> _CurrentImageLocator = RegisterContainerLocator<Models.MemoryStreamBase64Item>("CurrentImage", model => model.Initialize("CurrentImage", ref model._CurrentImage, ref _CurrentImageLocator, _CurrentImageDefaultValueFactory));
		static Func<Models.MemoryStreamBase64Item> _CurrentImageDefaultValueFactory = () => default(Models.MemoryStreamBase64Item);
		#endregion

		 

																		   



		public string ImageResult
		{
			get { return _ImageResultLocator(this).Value; }
			set { _ImageResultLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string ImageResult Setup        
		protected Property<string> _ImageResult = new Property<string> { LocatorFunc = _ImageResultLocator };
		static Func<BindableBase, ValueContainer<string>> _ImageResultLocator = RegisterContainerLocator<string>(nameof(ImageResult), model => model.Initialize(nameof(ImageResult), ref model._ImageResult, ref _ImageResultLocator, _ImageResultDefaultValueFactory));
		static Func<string> _ImageResultDefaultValueFactory = () => default(string);
		#endregion



		public Color CurrentPenColor
		{
			get { return _CurrentPenColorLocator(this).Value; }
			set { _CurrentPenColorLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Color CurrentPenColor Setup        
		protected Property<Color> _CurrentPenColor = new Property<Color> { LocatorFunc = _CurrentPenColorLocator };
		static Func<BindableBase, ValueContainer<Color>> _CurrentPenColorLocator = RegisterContainerLocator<Color>(nameof(CurrentPenColor), model => model.Initialize(nameof(CurrentPenColor), ref model._CurrentPenColor, ref _CurrentPenColorLocator, _CurrentPenColorDefaultValueFactory));
		static Func<Color> _CurrentPenColorDefaultValueFactory = () => default(Color);
		#endregion



	}

}

