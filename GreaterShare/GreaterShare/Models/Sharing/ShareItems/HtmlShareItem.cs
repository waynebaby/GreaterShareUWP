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
	public class HtmlShareItem : BindableBase<HtmlShareItem>, IShareItem
	{

		//[DataMember]
		//public string HtmlFormat
		//{
		//	get { return _HtmlFormatLocator(this).Value; }
		//	set { _HtmlFormatLocator(this).SetValueAndTryNotify(value); }
		//}
		//#region Property string HtmlFormat Setup        
		//protected Property<string> _HtmlFormat = new Property<string> { LocatorFunc = _HtmlFormatLocator };
		//static Func<BindableBase, ValueContainer<string>> _HtmlFormatLocator = RegisterContainerLocator<string>(nameof(HtmlFormat), model => model.Initialize(nameof(HtmlFormat), ref model._HtmlFormat, ref _HtmlFormatLocator, _HtmlFormatDefaultValueFactory));
		//static Func<string> _HtmlFormatDefaultValueFactory = () => default(string);
		//#endregion

	

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
	}
}
