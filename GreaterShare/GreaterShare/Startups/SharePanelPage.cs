using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using GreaterShare;
using GreaterShare.ViewModels;
using System;
using System.Net;
using System.Windows;


namespace MVVMSidekick.Startups
{
	internal static partial class StartupFunctions
	{
		static Action SharePanelPageConfig =
		   CreateAndAddToAllConfig(ConfigSharePanelPage);

		public static void ConfigSharePanelPage()
		{
			ViewModelLocator<SharePanelPage_Model>
				.Instance
				.Register(context =>
					new SharePanelPage_Model())
				.GetViewMapper()
				.MapToDefault<SharePanelPage>();

		}
	}
}
