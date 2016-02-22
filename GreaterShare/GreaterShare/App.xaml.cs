using GreaterShare.Models.Sharing.ShareItems;
using GreaterShare.Services;
using GreaterShare.ViewModels;
using MVVMSidekick.EventRouting;
using MVVMSidekick.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=402347&clcid=0x409

namespace GreaterShare
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	sealed partial class App : Application
	{
		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			this.InitializeComponent();
			this.Suspending += OnSuspending;
			MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<Exception>()
				.Subscribe(
				e => Debug.Write(e.EventData));
		}




		public static void InitNavigationConfigurationInThisAssembly()
		{
			MVVMSidekick.Startups.StartupFunctions.RunAllConfig();
			ServiceLocator.Instance.Register<IShareService, DefaultShareService>();
			ServiceLocator.Instance.Register<ISubStorageService, DefaultSubStorageService>();
			RuntimeHelpers.RunClassConstructor(typeof (TextShareItem).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(WebLinkShareItem).TypeHandle);

		}

		public static BehaviorSubject<IStorageItem> CurrentFile
					= new BehaviorSubject<IStorageItem>(null);

		public static string FileExtension { get; internal set; }
		= ".gshare";

		public static Task<Uri> CurrentAppUri = Task.Factory.StartNew(()=>
		{
			return new Uri("https://www.microsoft.com/store/apps/9nblggh5fmm2");
		});

		protected override void OnFileActivated(FileActivatedEventArgs args)
		{

			InitNavigationConfigurationInThisAssembly();
			Frame rootFrame = CreateRootFrame();

			if (rootFrame.Content == null)
			{
				rootFrame.Navigate(typeof(MainPage), null);
			}

			//EventRouter.Instance.GetEventChannel<MainPage_Model>()
			//	.Where(e => e.EventName == "Loaded")
			//	.Subscribe(
			//	e =>
			//	 {
			//		 CurrentFile.OnNext(args.Files.FirstOrDefault());
			//	 }
			//	);

			ConfigTitlebar();

			CurrentFile.OnNext(args.Files.FirstOrDefault());
			Window.Current.Activate();
			//base.OnFileActivated(args);
		}


		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="e">Details about the launch request and process.</param>
		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{

#if DEBUG
			if (System.Diagnostics.Debugger.IsAttached)
			{
				this.DebugSettings.EnableFrameRateCounter = true;
			}
#endif
			//Init MVVM-Sidekick Navigations:
			InitNavigationConfigurationInThisAssembly();
			Frame rootFrame = CreateRootFrame();

			if (rootFrame.Content == null)
			{
				// When the navigation stack isn't restored navigate to the first page,
				// configuring the new page by passing required information as a navigation
				// parameter
				rootFrame.Navigate(typeof(MainPage), e.Arguments);
			}
			// Ensure the current window is active

			CurrentFile.OnNext(null);
			ConfigTitlebar();
			Window.Current.Activate();
		}

		private static void ConfigTitlebar()
		{
			var view = CoreApplication.GetCurrentView();
			CoreApplicationViewTitleBar coreTitleBar = view.TitleBar;
			coreTitleBar.ExtendViewIntoTitleBar = true;
			ApplicationViewTitleBar formatableTitlebar = ApplicationView.GetForCurrentView().TitleBar;
			formatableTitlebar.ButtonBackgroundColor = Color.FromArgb(64, 64, 64, 64);
		}


		/// <summary>
		/// Invoked when Navigation to a certain page fails
		/// </summary>
		/// <param name="sender">The Frame which failed navigation</param>
		/// <param name="e">Details about the navigation failure</param>
		void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		/// <summary>
		/// Invoked when application execution is being suspended.  Application state is saved
		/// without knowing whether the application will be terminated or resumed with the contents
		/// of memory still intact.
		/// </summary>
		/// <param name="sender">The source of the suspend request.</param>
		/// <param name="e">Details about the suspend request.</param>
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			//TODO: Save application state and stop any background activity
			deferral.Complete();
		}

		private Frame CreateRootFrame()
		{
			Frame rootFrame = Window.Current.Content as Frame;

			// Do not repeat app initialization when the Window already has content,
			// just ensure that the window is active
			if (rootFrame == null)
			{
				// Create a Frame to act as the navigation context and navigate to the first page
				rootFrame = new Frame();

				// Set the default language
				rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
				rootFrame.NavigationFailed += OnNavigationFailed;

				// Place the frame in the current Window
				Window.Current.Content = rootFrame;
			}

			return rootFrame;
		}

		protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
		{


			InitNavigationConfigurationInThisAssembly();
			var rootFrame = CreateRootFrame();

			// When the navigation stack isn't restored navigate to the first page,
			// configuring the new page by passing required information as a navigation
			// parameter
			rootFrame.Navigate(typeof(SharePanelPage), args.ShareOperation);


			Window.Current.Activate();
			base.OnShareTargetActivated(args);
			
		}



	}


}
