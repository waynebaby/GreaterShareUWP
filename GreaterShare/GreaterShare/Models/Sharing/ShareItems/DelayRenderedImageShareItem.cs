using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.Models.Sharing.ShareItems
{
public 	class DelayRenderedImageShareItem	:ShareItemBase<DelayRenderedImageShareItem>
	{

		public string SelectedImageFile
		{
			get { return _SelectedImageFileLocator(this).Value; }
			set { _SelectedImageFileLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string SelectedImageFile Setup        
		protected Property<string> _SelectedImageFile = new Property<string> { LocatorFunc = _SelectedImageFileLocator };
		static Func<BindableBase, ValueContainer<string>> _SelectedImageFileLocator = RegisterContainerLocator<string>(nameof(SelectedImageFile), model => model.Initialize(nameof(SelectedImageFile), ref model._SelectedImageFile, ref _SelectedImageFileLocator, _SelectedImageFileDefaultValueFactory));
		static Func<string> _SelectedImageFileDefaultValueFactory = () => default(string);
		#endregion

	}
}
