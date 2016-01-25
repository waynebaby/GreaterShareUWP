using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.Models.Sharing.ShareItems
{
	public class HtmlShareItem : ShareItemBase<HtmlShareItem>
	{


		public string HtmlFormat
		{
			get { return _HtmlFormatLocator(this).Value; }
			set { _HtmlFormatLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string HtmlFormat Setup        
		protected Property<string> _HtmlFormat = new Property<string> { LocatorFunc = _HtmlFormatLocator };
		static Func<BindableBase, ValueContainer<string>> _HtmlFormatLocator = RegisterContainerLocator<string>(nameof(HtmlFormat), model => model.Initialize(nameof(HtmlFormat), ref model._HtmlFormat, ref _HtmlFormatLocator, _HtmlFormatDefaultValueFactory));
		static Func<string> _HtmlFormatDefaultValueFactory = () => default(string);
		#endregion

	}
}
