using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.Models.ClipboardData.ContentEntries
{
	public abstract class ClipboardContentDataEntryBase<TSubClass> : BindableBase<TSubClass>, IClipboardContentDataEntry where TSubClass : ClipboardContentDataEntryBase<TSubClass>
	{

		public string FormatName
		{
			get { return _FormatNameLocator(this).Value; }
			set { _FormatNameLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string FormatName Setup        
		protected Property<string> _FormatName = new Property<string> { LocatorFunc = _FormatNameLocator };
		static Func<BindableBase, ValueContainer<string>> _FormatNameLocator = RegisterContainerLocator<string>(nameof(FormatName), model => model.Initialize(nameof(FormatName), ref model._FormatName, ref _FormatNameLocator, _FormatNameDefaultValueFactory));
		static Func<string> _FormatNameDefaultValueFactory = () => default(string);
		#endregion


		public Object EntryObject
		{
			get { return _EntryObjectLocator(this).Value; }
			set { _EntryObjectLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Object EntryObject Setup        
		protected Property<Object> _EntryObject = new Property<Object> { LocatorFunc = _EntryObjectLocator };
		static Func<BindableBase, ValueContainer<Object>> _EntryObjectLocator = RegisterContainerLocator<Object>(nameof(EntryObject), model => model.Initialize(nameof(EntryObject), ref model._EntryObject, ref _EntryObjectLocator, _EntryObjectDefaultValueFactory));
		static Func<Object> _EntryObjectDefaultValueFactory = () => default(Object);
		#endregion

	}

}
