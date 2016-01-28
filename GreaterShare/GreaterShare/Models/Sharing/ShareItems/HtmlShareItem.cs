using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.Models.Sharing.ShareItems
{
	[DataContract]
	public class HtmlShareItem : BindableBase<HtmlShareItem>
	{

		[DataMember]
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

		[DataMember]				
		public Uri WebLink
		{
			get { return _WebLinkLocator(this).Value; }
			set { _WebLinkLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Uri WebLink Setup        
		protected Property<Uri> _WebLink = new Property<Uri> { LocatorFunc = _WebLinkLocator };
		static Func<BindableBase, ValueContainer<Uri>> _WebLinkLocator = RegisterContainerLocator<Uri>(nameof(WebLink), model => model.Initialize(nameof(WebLink), ref model._WebLink, ref _WebLinkLocator, _WebLinkDefaultValueFactory));
		static Func<Uri> _WebLinkDefaultValueFactory = () => default(Uri);
		#endregion

		[DataMember]

		public string HtmlFragment
		{
			get { return _HtmlFragmentLocator(this).Value; }
			set { _HtmlFragmentLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string HtmlFragment Setup        
		protected Property<string> _HtmlFragment = new Property<string> { LocatorFunc = _HtmlFragmentLocator };
		static Func<BindableBase, ValueContainer<string>> _HtmlFragmentLocator = RegisterContainerLocator<string>(nameof(HtmlFragment), model => model.Initialize(nameof(HtmlFragment), ref model._HtmlFragment, ref _HtmlFragmentLocator, _HtmlFragmentDefaultValueFactory));
		static Func<string> _HtmlFragmentDefaultValueFactory = () => default(string);
		#endregion

	}
}
