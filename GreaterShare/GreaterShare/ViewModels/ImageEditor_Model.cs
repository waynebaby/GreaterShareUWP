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

		 


		public ObservableCollection<Color> BrushColors
		{
			get { return _BrushColorsLocator(this).Value; }
			set { _BrushColorsLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property ObservableCollection<Color> BrushColors Setup        
		protected Property<ObservableCollection<Color>> _BrushColors = new Property<ObservableCollection<Color>> { LocatorFunc = _BrushColorsLocator };
		static Func<BindableBase, ValueContainer<ObservableCollection<Color>>> _BrushColorsLocator = RegisterContainerLocator<ObservableCollection<Color>>(nameof(BrushColors), model => model.Initialize(nameof(BrushColors), ref model._BrushColors, ref _BrushColorsLocator, _BrushColorsDefaultValueFactory));
		static Func<ObservableCollection<Color>> _BrushColorsDefaultValueFactory = () => new ObservableCollection<Color> {
			Colors.Black,
			Colors.Yellow,
			Colors.Green,
			Colors.Red,
			Colors.LightGoldenrodYellow,
			Colors.PaleVioletRed,
			Colors.LawnGreen,
			Colors.YellowGreen
		};
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





		public Guid CurrentVersion
		{
			get { return _CurrentVersionLocator(this).Value; }
			set { _CurrentVersionLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Guid CurrentVersion Setup        
		protected Property<Guid> _CurrentVersion = new Property<Guid> { LocatorFunc = _CurrentVersionLocator };
		static Func<BindableBase, ValueContainer<Guid>> _CurrentVersionLocator = RegisterContainerLocator<Guid>(nameof(CurrentVersion), model => model.Initialize(nameof(CurrentVersion), ref model._CurrentVersion, ref _CurrentVersionLocator, _CurrentVersionDefaultValueFactory));
		static Func<Guid> _CurrentVersionDefaultValueFactory = () => default(Guid);
		#endregion




	}

}

