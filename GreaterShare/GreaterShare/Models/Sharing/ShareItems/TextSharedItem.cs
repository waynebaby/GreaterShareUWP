using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.Models.Sharing.ShareItems
{
	[DataContract]public class TextSharedItem	   :ShareItemBase<TextSharedItem>
	{

		[DataMember]

		public string Text
		{
			get { return _TextLocator(this).Value; }
			set { _TextLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string Text Setup        
		protected Property<string> _Text = new Property<string> { LocatorFunc = _TextLocator };
		static Func<BindableBase, ValueContainer<string>> _TextLocator = RegisterContainerLocator<string>(nameof(Text), model => model.Initialize(nameof(Text), ref model._Text, ref _TextLocator, _TextDefaultValueFactory));
		static Func<string> _TextDefaultValueFactory = () => default(string);
		#endregion

	}
}
