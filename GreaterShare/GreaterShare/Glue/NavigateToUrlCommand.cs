using MVVMSidekick.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using System.Reactive.Linq;
namespace GreaterShare.Glue
{
	public class NavigateToUrlCommand : ReactiveCommand
	{

		public NavigateToUrlCommand() : base(true)
		{
			this
				.ObserveOnDispatcher()
				.Subscribe(
				async e =>
				{
					if (e.EventArgs.Parameter is Uri)
					{
						await Launcher.LaunchUriAsync(e.EventArgs.Parameter as Uri);
					}

				});

		}
	}
}
