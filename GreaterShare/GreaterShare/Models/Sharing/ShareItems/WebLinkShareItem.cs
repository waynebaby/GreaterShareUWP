using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.Models.Sharing.ShareItems
{
	[DataContract]
	public class WebLinkShareItem : BindableBase<WebLinkShareItem>, IShareItem
	{
		public WebLinkShareItem()
		{
			WireEvent();
		}
		protected override void OnDeserializingActions()
		{
			base.OnDeserializingActions();
			WireEvent();
		}


		bool _IsExpectingRemoveFromHistory = false;
		public bool IsEventWired { get; set; } = false;

		public void WireEvent()
		{
			if (!IsEventWired)
			{
				this.GetValueContainer(x => x.WebLink).GetEventObservable()
				.Subscribe(
					e =>
					{
						var link = this;
						var wrongUrlsegment = "&geoMarket";
						//var rightUrlsegment = "&geoMarket";
						int index = -1;
						if (link != null &&
							link.WebLink != null &&
							!link.WebLink.PathAndQuery.Contains("?") &&
							(index = link.WebLink.PathAndQuery.IndexOf(wrongUrlsegment)) != -1)
						{
							var ub = new UriBuilder(link.WebLink);
							var removed = link.WebLink.PathAndQuery.Substring(index);
							ub.Path = link.WebLink.PathAndQuery.Remove(index);
							ub.Query = "fixedByGreaterShare=1" + removed;
							link.WebLink = ub.Uri;
						}

						if (!_IsExpectingRemoveFromHistory)
						{
							History.Add(e.EventArgs.OldValue);
							while (History.Count > 20)
							{
								History.RemoveAt(0);
							}
						}
						else
						{

							_IsExpectingRemoveFromHistory = false;
						}
					}
				)
				.DisposeWith(this);
				//CommandBackHistory.CommandCore.ListenCanExecuteObservable(History
				//	.GetEventObservable(this)
				//	.Select(x =>
				//			History.Count > 1));
				IsEventWired = true;
			}
		}
		[DataMember]

		public Uri WebLink
		{
			get { return _WebLinkLocator(this).Value; }
			set { _WebLinkLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property Uri WebLink Setup        
		protected Property<Uri> _WebLink = new Property<Uri> { LocatorFunc = _WebLinkLocator };
		static Func<BindableBase, ValueContainer<Uri>> _WebLinkLocator = RegisterContainerLocator<Uri>(nameof(WebLink), model => model.Initialize(nameof(WebLink), ref model._WebLink, ref _WebLinkLocator, _WebLinkDefaultValueFactory));
		static Func<Uri> _WebLinkDefaultValueFactory = () => default(Uri);
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


		public ObservableCollection<Uri> History
		{
			get { return _HistoryLocator(this).Value; }
			set { _HistoryLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property ObservableCollection<Uri> History Setup        
		protected Property<ObservableCollection<Uri>> _History = new Property<ObservableCollection<Uri>> { LocatorFunc = _HistoryLocator };
		static Func<BindableBase, ValueContainer<ObservableCollection<Uri>>> _HistoryLocator = RegisterContainerLocator<ObservableCollection<Uri>>(nameof(History), model => model.Initialize(nameof(History), ref model._History, ref _HistoryLocator, _HistoryDefaultValueFactory));
		static Func<ObservableCollection<Uri>> _HistoryDefaultValueFactory = () => new ObservableCollection<Uri>();
		#endregion



		public CommandModel<ReactiveCommand, String> CommandBackHistory
		{
			get { return _CommandBackHistoryLocator(this).Value; }
			set { _CommandBackHistoryLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandBackHistory Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandBackHistory = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandBackHistoryLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandBackHistoryLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandBackHistory", model => model.Initialize("CommandBackHistory", ref model._CommandBackHistory, ref _CommandBackHistoryLocator, _CommandBackHistoryDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandBackHistoryDefaultValueFactory =
			model =>
			{
				var resource = "CommandBackHistory";           // Command resource  
				var commandId = "CommandBackHistory";
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.Do(e =>
				{
					if (vm.History.Count <= 1)
					{
						return;
					}
					var last = vm.History.Count - 1;
					var oldOne = vm.History[last];
					vm.History.RemoveAt(vm.History.Count - 1);
					vm._IsExpectingRemoveFromHistory = true;
					vm.WebLink = oldOne;

					//Todo: Add BackHistory logic here
				})
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				//cmd.ListenCanExecuteObservable(vm.History
				//	.GetEventObservable(vm)
				//	.Select(x =>
				//	vm.History.Count > 1));

				var cmdmdl = cmd.CreateCommandModel(resource);


				return cmdmdl;
			};

		#endregion



		public CommandModel<ReactiveCommand, String> CommandRemoveQueryParams
		{
			get { return _CommandRemoveQueryParamsLocator(this).Value; }
			set { _CommandRemoveQueryParamsLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandRemoveQueryParams Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandRemoveQueryParams = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandRemoveQueryParamsLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandRemoveQueryParamsLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandRemoveQueryParams), model => model.Initialize(nameof(CommandRemoveQueryParams), ref model._CommandRemoveQueryParams, ref _CommandRemoveQueryParamsLocator, _CommandRemoveQueryParamsDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandRemoveQueryParamsDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandRemoveQueryParams);           // Command resource  
				var commandId = nameof(CommandRemoveQueryParams);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.Where(e => vm.WebLink != null)
					.Do(e =>
					 {
						 var ub = new UriBuilder(vm.WebLink);
						 ub.Query = null;
						 vm.WebLink = ub.Uri;
					 }
					)
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				var cmdmdl = cmd.CreateCommandModel(resource);

				//cmdmdl.ListenToIsUIBusy(
				//	model: vm,
				//	canExecuteWhenBusy: false);
				return cmdmdl;
			};

		#endregion



	}
}
