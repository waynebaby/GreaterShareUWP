using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace GreaterShare.Models.Sharing
{

	[DataContract()] //if you want
	public abstract class ShareItemBase<TSubclass> : BindableBase<TSubclass>, IShareItem where TSubclass : ShareItemBase<TSubclass>
	{

		//[DataMember]
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

		//[DataMember]

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
		//[DataMember]

		public Uri ContentSourceApplicationLink
		{
			get { return _ContentSourceApplicationLinkLocator(this).Value; }
			set { _ContentSourceApplicationLinkLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Uri ContentSourceApplicationLink Setup        
		protected Property<Uri> _ContentSourceApplicationLink = new Property<Uri> { LocatorFunc = _ContentSourceApplicationLinkLocator };
		static Func<BindableBase, ValueContainer<Uri>> _ContentSourceApplicationLinkLocator = RegisterContainerLocator<Uri>(nameof(ContentSourceApplicationLink), model => model.Initialize(nameof(ContentSourceApplicationLink), ref model._ContentSourceApplicationLink, ref _ContentSourceApplicationLinkLocator, _ContentSourceApplicationLinkDefaultValueFactory));
		static Func<Uri> _ContentSourceApplicationLinkDefaultValueFactory = () => default(Uri);
		#endregion

		//[DataMember]		  
		public Uri ContentSourceWebLink
		{
			get { return _ContentSourceWebLinkLocator(this).Value; }
			set { _ContentSourceWebLinkLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Uri ContentSourceWebLink Setup        
		protected Property<Uri> _ContentSourceWebLink = new Property<Uri> { LocatorFunc = _ContentSourceWebLinkLocator };
		static Func<BindableBase, ValueContainer<Uri>> _ContentSourceWebLinkLocator = RegisterContainerLocator<Uri>(nameof(ContentSourceWebLink), model => model.Initialize(nameof(ContentSourceWebLink), ref model._ContentSourceWebLink, ref _ContentSourceWebLinkLocator, _ContentSourceWebLinkDefaultValueFactory));
		static Func<Uri> _ContentSourceWebLinkDefaultValueFactory = () => default(Uri);
		#endregion


		/// <summary>
		/// Most of Apps won't care this
		/// </summary>		   
		//[DataMember]

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
