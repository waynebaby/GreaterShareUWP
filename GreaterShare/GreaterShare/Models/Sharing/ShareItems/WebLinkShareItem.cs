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
	public class WebLinkShareItem : BindableBase<WebLinkShareItem>
	{
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



	}
}
