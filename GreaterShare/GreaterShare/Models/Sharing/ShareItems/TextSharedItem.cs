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
	public class TextSharedItem : BindableBase<TextSharedItem>, IShareItem
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
