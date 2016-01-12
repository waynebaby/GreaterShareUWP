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
	public class ApplicationLinkShareItem : ShareItemBase<ApplicationLinkShareItem>
	{

		[DataMember]
		public Uri ApplicationLink
		{
			get { return _ApplicationLinkLocator(this).Value; }
			set { _ApplicationLinkLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Uri ApplicationLink Setup        
		protected Property<Uri> _ApplicationLink = new Property<Uri> { LocatorFunc = _ApplicationLinkLocator };
		static Func<BindableBase, ValueContainer<Uri>> _ApplicationLinkLocator = RegisterContainerLocator<Uri>(nameof(ApplicationLink), model => model.Initialize(nameof(ApplicationLink), ref model._ApplicationLink, ref _ApplicationLinkLocator, _ApplicationLinkDefaultValueFactory));
		static Func<Uri> _ApplicationLinkDefaultValueFactory = () => default(Uri);
		#endregion


	}
}
