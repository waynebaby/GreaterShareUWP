using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace GreaterShare.Models.ClipboardData.ContentEntries
{
	public class HtmlEntry : ClipboardContentDataEntryBase<HtmlEntry>
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



		public IReadOnlyDictionary<string, RandomAccessStreamReference> ResourceMap
		{
			get { return _ResourceMapLocator(this).Value; }
			set { _ResourceMapLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property IReadOnlyDictionary<string, RandomAccessStreamReference> ResourceMap Setup        
		protected Property<IReadOnlyDictionary<string, RandomAccessStreamReference>> _ResourceMap = new Property<IReadOnlyDictionary<string, RandomAccessStreamReference>> { LocatorFunc = _ResourceMapLocator };
		static Func<BindableBase, ValueContainer<IReadOnlyDictionary<string, RandomAccessStreamReference>>> _ResourceMapLocator = RegisterContainerLocator<IReadOnlyDictionary<string, RandomAccessStreamReference>>(nameof(ResourceMap), model => model.Initialize(nameof(ResourceMap), ref model._ResourceMap, ref _ResourceMapLocator, _ResourceMapDefaultValueFactory));
		static Func<IReadOnlyDictionary<string, RandomAccessStreamReference>> _ResourceMapDefaultValueFactory = () => default(IReadOnlyDictionary<string, RandomAccessStreamReference>);
		#endregion

	}
}
