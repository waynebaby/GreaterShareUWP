
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
using Windows.ApplicationModel.DataTransfer.ShareTarget;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GreaterShare
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class SharePanelPage : MVVMPage
	{



		public SharePanelPage()
		{

			this.InitializeComponent();
			this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
			{
				StrongTypeViewModel = this.ViewModel as SharePanelPage_Model;
			});
			StrongTypeViewModel = this.ViewModel as SharePanelPage_Model;
			Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences = Windows.Graphics.Display.DisplayOrientations.Portrait;
		}


		public SharePanelPage_Model StrongTypeViewModel
		{
			get { return (SharePanelPage_Model)GetValue(StrongTypeViewModelProperty); }
			set { SetValue(StrongTypeViewModelProperty, value); }
		}

		public static readonly DependencyProperty StrongTypeViewModelProperty =
					DependencyProperty.Register("StrongTypeViewModel", typeof(SharePanelPage_Model), typeof(SharePanelPage), new PropertyMetadata(null));




		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			StrongTypeViewModel.SharedOperation = e.Parameter as ShareOperation;
			base.OnNavigatedTo(e);
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
		}

	}
}
