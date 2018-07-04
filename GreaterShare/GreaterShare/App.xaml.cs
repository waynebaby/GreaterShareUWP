﻿using GreaterShare.Models.Sharing.ShareItems;
using GreaterShare.Services;
using Microsoft.HockeyApp;
using MVVMSidekick.Commands;
using MVVMSidekick.EventRouting;
using MVVMSidekick.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Edi.UWP.Helpers;

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
        /// </summary>tion
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            HockeyClient.Current.Configure(
                "e5bfbcda5d364b3382971fa8176f2d90",
                new TelemetryConfiguration
                {
                    EnableDiagnostics = true,
                    Collectors = WindowsCollectors.Metadata | WindowsCollectors.PageView | WindowsCollectors.Session | WindowsCollectors.UnhandledException | WindowsCollectors.WatsonData,
                    DescriptionLoader = (ex) => "HResult = " + ex.HResult.ToString()
                });
        }

        static bool _inited = false;
        public static void InitConfigurationInThisAssembly()
        {
            if (!_inited)
            {
                MVVMSidekick.Startups.StartupFunctions.RunAllConfig();
                ConfigureCommandAndCommandExceptionHandler();
                ServiceLocator.Instance.Register<IDrawingService, DrawingService>();
                ServiceLocator.Instance.Register<IShareService, DefaultShareService>();
                ServiceLocator.Instance.Register<ISubStorageService, DefaultSubStorageService>();
                ServiceLocator.Instance.Register<IImageConvertService, ConvertImageToPNGService>();
                RuntimeHelpers.RunClassConstructor(typeof(TextShareItem).TypeHandle);
                RuntimeHelpers.RunClassConstructor(typeof(WebLinkShareItem).TypeHandle);
                _inited = true;
            }

        }

        public static BehaviorSubject<IStorageItem> CurrentFile
                    = new BehaviorSubject<IStorageItem>(null);

        public static string FileExtension { get; internal set; } = ".gshare";

        public static Task<Uri> CurrentAppUri = Task.Factory.StartNew(() => new Uri("https://www.microsoft.com/store/apps/9nblggh5fmm2"));

        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            InitConfigurationInThisAssembly();
            Frame rootFrame = CreatOrSetupeRootFrame();
            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(MainPage), null);
            }

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
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {

//#if DEBUG
//            if (System.Diagnostics.Debugger.IsAttached)
//            {
//                this.DebugSettings.EnableFrameRateCounter = true;
//            }
//#endif

            //Init MVVM-Sidekick Navigations:
            InitConfigurationInThisAssembly();
            Frame rootFrame = CreatOrSetupeRootFrame();

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
            SetTitleBarColor();
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

        public static void SetTitleBarColor()
        {
            var color = Edi.UWP.Helpers.UI.GetAccentColor();

            //var colorStr = colorHex.Replace("#", string.Empty);
            //var r = (byte)(Convert.ToUInt32(colorStr.Substring(0, 2), 16));
            //var g = (byte)(Convert.ToUInt32(colorStr.Substring(2, 2), 16));
            //var b = (byte)(Convert.ToUInt32(colorStr.Substring(4, 2), 16));
            //var color = Color.FromArgb(255, r, g, b);

            Mobile.SetWindowsMobileStatusBarColor(color, Colors.White);

            UI.ApplyColorToTitleBar(
                color,
                Colors.White,
                Colors.LightGray,
                Colors.Gray);

            UI.ApplyColorToTitleButton(
                color, Colors.White,
                color, Colors.White,
                color, Colors.White,
                Colors.LightGray, Colors.Gray);
        }


        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            var template = ResourceLoader.GetForViewIndependentUse().GetString(nameof(OnNavigationFailed) + nameof(Exception));
            throw new Exception(string.Format(template, e.SourcePageType.FullName));
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

        private Frame CreatOrSetupeRootFrame()
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
            InitConfigurationInThisAssembly();
            var rootFrame = CreatOrSetupeRootFrame();

            // When the navigation stack isn't restored navigate to the first page,
            // configuring the new page by passing required information as a navigation
            // parameter
            rootFrame.Navigate(typeof(SharePanelPage), args.ShareOperation);


            Window.Current.Activate();
            base.OnShareTargetActivated(args);
        }

        /// <summary>
        /// Configure event handler when command executed or exception happens
        /// </summary>
        private static void ConfigureCommandAndCommandExceptionHandler()
        {
            EventRouter.Instance.GetEventChannel<EventPattern<EventCommandEventArgs>>()
                .ObserveOnDispatcher()
                .Subscribe(
                    e =>
                    {
                        //Command Fired Messages
                        HockeyClient.Current.TrackEvent($"Command.{e.EventName ?? "none"})");
                    }
                );

            EventRouter.Instance.GetEventChannel<Exception>()
                .ObserveOnDispatcher()
                .Subscribe(
                    e =>
                    {
                        //Exceptions Messages 
                        if (Exceptions.Count >= 20)
                        {
                            Exceptions.RemoveAt(0);
                        }
                        Exceptions.Add(Tuple.Create(DateTime.Now, e.EventData));
                        Debug.WriteLine(e.EventData);
                        HockeyClient.Current.TrackEvent($"CommandExp.{e.EventName ?? "none"}.{e.EventData.Message} ");

                    }
                );
        }

        /// <summary>
        /// Exception lists
        /// </summary>
        public static ObservableCollection<Tuple<DateTime, Exception>> Exceptions { get; set; } = new ObservableCollection<Tuple<DateTime, Exception>>();
    }
}
