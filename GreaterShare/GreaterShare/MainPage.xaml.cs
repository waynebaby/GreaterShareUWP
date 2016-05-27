using MVVMSidekick.Views;
using Windows.UI.Xaml;
using GreaterShare.ViewModels;

namespace GreaterShare
{
	public sealed partial class MainPage : MVVMPage
	{
		public MainPage()
		{
			this.InitializeComponent();

			this.ViewModel = ViewModelLocator<MainPage_Model>.Instance.Resolve();
			this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
			{
				StrongTypeViewModel = this.ViewModel as MainPage_Model;
			});				  
			StrongTypeViewModel = this.ViewModel as MainPage_Model;
		}

		public MainPage_Model StrongTypeViewModel
		{
			get { return (MainPage_Model)GetValue(StrongTypeViewModelProperty); }
			set { SetValue(StrongTypeViewModelProperty, value); }
			
		}

		public static readonly DependencyProperty StrongTypeViewModelProperty =
					DependencyProperty.Register(nameof(StrongTypeViewModel), typeof(MainPage_Model), typeof(MainPage), new PropertyMetadata(null));
	}
}
