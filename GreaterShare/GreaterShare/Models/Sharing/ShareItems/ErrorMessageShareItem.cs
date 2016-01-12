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
	public class ErrorMessageShareItem : ShareItemBase<ErrorMessageShareItem>
	{

		[DataMember]			  
		public String CustomErrorText
		{
			get { return _CustomErrorTextLocator(this).Value; }
			set { _CustomErrorTextLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property String CustomErrorText Setup        
		protected Property<String> _CustomErrorText = new Property<String> { LocatorFunc = _CustomErrorTextLocator };
		static Func<BindableBase, ValueContainer<String>> _CustomErrorTextLocator = RegisterContainerLocator<String>(nameof(CustomErrorText), model => model.Initialize(nameof(CustomErrorText), ref model._CustomErrorText, ref _CustomErrorTextLocator, _CustomErrorTextDefaultValueFactory));
		static Func<String> _CustomErrorTextDefaultValueFactory = () => default(String);
		#endregion

	}
}
