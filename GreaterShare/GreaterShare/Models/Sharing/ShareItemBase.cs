using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.Models.Sharing
{

	//[DataContract() ] //if you want
	public abstract class ShareItemBase<TSubclass> : BindableBase<TSubclass>, IShareItem where TSubclass : ShareItemBase<TSubclass>
	{

		public string Title
		{
			get { return _TitleLocator(this).Value; }
			set { _TitleLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string Title Setup        
		protected Property<string> _Title = new Property<string> { LocatorFunc = _TitleLocator };
		static Func<BindableBase, ValueContainer<string>> _TitleLocator = RegisterContainerLocator<string>(nameof(Title), model => model.Initialize(nameof(Title), ref model._Title, ref _TitleLocator, _TitleDefaultValueFactory));
		static Func<string> _TitleDefaultValueFactory = () => default(string);
		#endregion



		public string Description
		{
			get { return _DescriptionLocator(this).Value; }
			set { _DescriptionLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string Description Setup        
		protected Property<string> _Description = new Property<string> { LocatorFunc = _DescriptionLocator };
		static Func<BindableBase, ValueContainer<string>> _DescriptionLocator = RegisterContainerLocator<string>(nameof(Description), model => model.Initialize(nameof(Description), ref model._Description, ref _DescriptionLocator, _DescriptionDefaultValueFactory));
		static Func<string> _DescriptionDefaultValueFactory = () => default(string);
		#endregion



											   /// <summary>
											   /// Most of Apps won't care this
											   /// </summary>
		public string ContentSourceApplicationLink
		{
			get { return _ContentSourceApplicationLinkLocator(this).Value; }
			set { _ContentSourceApplicationLinkLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string ContentSourceApplicationLink Setup        
		protected Property<string> _ContentSourceApplicationLink = new Property<string> { LocatorFunc = _ContentSourceApplicationLinkLocator };
		static Func<BindableBase, ValueContainer<string>> _ContentSourceApplicationLinkLocator = RegisterContainerLocator<string>(nameof(ContentSourceApplicationLink), model => model.Initialize(nameof(ContentSourceApplicationLink), ref model._ContentSourceApplicationLink, ref _ContentSourceApplicationLinkLocator, _ContentSourceApplicationLinkDefaultValueFactory));
		static Func<string> _ContentSourceApplicationLinkDefaultValueFactory = () => default(string);
		#endregion



		public string DefaultFailedDisplayText
		{
			get { return _DefaultFailedDisplayTextLocator(this).Value; }
			set { _DefaultFailedDisplayTextLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string DefaultFailedDisplayText Setup        
		protected Property<string> _DefaultFailedDisplayText = new Property<string> { LocatorFunc = _DefaultFailedDisplayTextLocator };
		static Func<BindableBase, ValueContainer<string>> _DefaultFailedDisplayTextLocator = RegisterContainerLocator<string>(nameof(DefaultFailedDisplayText), model => model.Initialize(nameof(DefaultFailedDisplayText), ref model._DefaultFailedDisplayText, ref _DefaultFailedDisplayTextLocator, _DefaultFailedDisplayTextDefaultValueFactory));
		static Func<string> _DefaultFailedDisplayTextDefaultValueFactory = () => default(string);
		#endregion



	}





}
