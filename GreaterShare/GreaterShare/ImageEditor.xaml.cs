
using GreaterShare.ViewModels;
using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GreaterShare
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ImageEditor : MVVMPage
	{



		public ImageEditor()
		{
         

            this.InitializeComponent();
			this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
			{
				StrongTypeViewModel = this.ViewModel as ImageEditor_Model;
			});
			StrongTypeViewModel = this.ViewModel as ImageEditor_Model;

			//this.Width = Window.Current.Bounds.Width;
			//this.Height = Window.Current.Bounds.Height;

		}


		public ImageEditor_Model StrongTypeViewModel
		{
			get { return (ImageEditor_Model)GetValue(StrongTypeViewModelProperty); }
			set { SetValue(StrongTypeViewModelProperty, value); }
		}

		public static readonly DependencyProperty StrongTypeViewModelProperty =
					DependencyProperty.Register("StrongTypeViewModel", typeof(ImageEditor_Model), typeof(ImageEditor), new PropertyMetadata(null));




		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
		}																															 

	}
}
