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
	public class CustomDataShareItem : ShareItemBase<CustomDataShareItem>
	{

		[DataMember]
		public string DataPackageFormat
		{
			get { return _DataPackageFormatLocator(this).Value; }
			set { _DataPackageFormatLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string DataPackageFormat Setup        
		protected Property<string> _DataPackageFormat = new Property<string> { LocatorFunc = _DataPackageFormatLocator };
		static Func<BindableBase, ValueContainer<string>> _DataPackageFormatLocator = RegisterContainerLocator<string>(nameof(DataPackageFormat), model => model.Initialize(nameof(DataPackageFormat), ref model._DataPackageFormat, ref _DataPackageFormatLocator, _DataPackageFormatDefaultValueFactory));
		static Func<string> _DataPackageFormatDefaultValueFactory = () => default(string);
		#endregion

		[DataMember]				 
		public object DataPackage
		{
			get { return _DataPackageLocator(this).Value; }
			set { _DataPackageLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property object DataPackage Setup        
		protected Property<object> _DataPackage = new Property<object> { LocatorFunc = _DataPackageLocator };
		static Func<BindableBase, ValueContainer<object>> _DataPackageLocator = RegisterContainerLocator<object>(nameof(DataPackage), model => model.Initialize(nameof(DataPackage), ref model._DataPackage, ref _DataPackageLocator, _DataPackageDefaultValueFactory));
		static Func<object> _DataPackageDefaultValueFactory = () => default(object);
		#endregion


	}
}
