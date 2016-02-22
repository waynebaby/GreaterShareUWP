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
	public class ApplicationLinkShareItem : BindableBase<ApplicationLinkShareItem>, IShareItem
	{
		public ApplicationLinkShareItem()
		{
			WireEvent();
		}
		protected override void OnDeserializingActions()
		{
			base.OnDeserializingActions();
			WireEvent();
		}
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


		public bool IsSelected
		{
			get { return _IsSelectedLocator(this).Value; }
			set { _IsSelectedLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property bool IsSelected Setup        
		protected Property<bool> _IsSelected = new Property<bool> { LocatorFunc = _IsSelectedLocator };
		static Func<BindableBase, ValueContainer<bool>> _IsSelectedLocator = RegisterContainerLocator<bool>(nameof(IsSelected), model => model.Initialize(nameof(IsSelected), ref model._IsSelected, ref _IsSelectedLocator, _IsSelectedDefaultValueFactory));
		static Func<bool> _IsSelectedDefaultValueFactory = () => true;


		#endregion
		public void WireEvent()
		{
			if (!IsEventWired)
			{
				IsEventWired = true;
			}
		}

		public bool IsEventWired{get;set;}=false ;
	}
}
