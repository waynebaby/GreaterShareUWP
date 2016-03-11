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
		static Action ImageEditorConfig =
		   CreateAndAddToAllConfig(ConfigImageEditor);

		public static void ConfigImageEditor()
		{
			ViewModelLocator<ImageEditor_Model>
				.Instance
				.Register(context =>
					new ImageEditor_Model())
				.GetViewMapper()
				.MapToDefault<ImageEditor>();

		}
	}
}
