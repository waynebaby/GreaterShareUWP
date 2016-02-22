using GreaterShare.ViewModels;
using MVVMSidekick.EventRouting;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;

namespace GreaterShare.Models.Sharing.ShareItems
{
	[DataContract]
	public class TextShareItem : BindableBase<TextShareItem>, IShareItem
	{
		public TextShareItem()
		{
			WireEvent();
		}

		protected override void OnDeserializingActions()
		{
			base.OnDeserializingActions();
			WireEvent();
		}
		public bool IsEventWired { get; set; } = false;
		public void WireEvent()
		{
			if (!IsEventWired)
			{
				//RuntimeHelpers.RunClassConstructor(this.GetType().TypeHandle);
				this.GetValueContainer(x => x.Text).GetEventObservable()
					.Subscribe(
						e =>
						{
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



				IsEventWired = true;
			}
		}

		[DataMember]

		public string Text
		{
			get { return _TextLocator(this).Value; }
			set { _TextLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string Text Setup        
		protected Property<string> _Text = new Property<string> { LocatorFunc = _TextLocator };
		static Func<BindableBase, ValueContainer<string>> _TextLocator =
			RegisterContainerLocator<string>(nameof(Text), model => model.Initialize(nameof(Text), ref model._Text, ref _TextLocator, _TextDefaultValueFactory));
		static Func<string> _TextDefaultValueFactory = () =>
			default(string);
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





		public ObservableCollection<string> History
		{
			get { return _HistoryLocator(this).Value; }
			set { _HistoryLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property ObservableCollection<string> History Setup        
		protected Property<ObservableCollection<string>> _History = new Property<ObservableCollection<string>> { LocatorFunc = _HistoryLocator };
		static Func<BindableBase, ValueContainer<ObservableCollection<string>>> _HistoryLocator = RegisterContainerLocator<ObservableCollection<string>>("History", model => model.Initialize("History", ref model._History, ref _HistoryLocator, _HistoryDefaultValueFactory));
		static Func<ObservableCollection<string>> _HistoryDefaultValueFactory = () => new ObservableCollection<string>();
		#endregion

		bool _IsExpectingRemoveFromHistory = false;

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
						   if (vm.History.Count<=1)
						   {
							   return;
						   }
						   var last = vm.History.Count - 1;
						   var oldOne = vm.History[last];
						   vm.History.RemoveAt(vm.History.Count - 1);
						   vm._IsExpectingRemoveFromHistory = true;
						   vm.Text = oldOne;

						   //Todo: Add BackHistory logic here
					   })
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);


				//cmd.ListenCanExecuteObservable(vm.History
				//   .GetEventObservable(vm)
				//   .Select(x =>
				//		vm.History.Count > 1));

				var cmdmdl = cmd.CreateCommandModel(resource);
				return cmdmdl;
			};

		#endregion


		public CommandModel<ReactiveCommand, String> CommandToBase64
		{
			get { return _CommandToBase64Locator(this).Value; }
			set { _CommandToBase64Locator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandToBase64 Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandToBase64 = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandToBase64Locator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandToBase64Locator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandToBase64), model => model.Initialize(nameof(CommandToBase64), ref model._CommandToBase64, ref _CommandToBase64Locator, _CommandToBase64DefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandToBase64DefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandToBase64);           // Command resource  
				var commandId = nameof(CommandToBase64);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.Do(
						 e =>
						{

							try
							{
								var bytes = Encoding.UTF8.GetBytes(vm.Text);
								var base64 = Convert.ToBase64String(bytes);
								vm.Text = base64;
							}
							catch (Exception)
							{

							}
						})
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				var cmdmdl = cmd.CreateCommandModel(resource);

				return cmdmdl;
			};

		#endregion


		public CommandModel<ReactiveCommand, String> CommandFromBase64
		{
			get { return _CommandFromBase64Locator(this).Value; }
			set { _CommandFromBase64Locator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandFromBase64 Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandFromBase64 = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandFromBase64Locator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandFromBase64Locator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandFromBase64), model => model.Initialize(nameof(CommandFromBase64), ref model._CommandFromBase64, ref _CommandFromBase64Locator, _CommandFromBase64DefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandFromBase64DefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandFromBase64);           // Command resource  
				var commandId = nameof(CommandFromBase64);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.Do(e =>
					   {
						   try
						   {
							   var bytes = Convert.FromBase64String(vm.Text.Trim());
							   vm.Text = Encoding.UTF8.GetString(bytes);
						   }
						   catch (Exception)
						   {
						   }
					   })
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				var cmdmdl = cmd.CreateCommandModel(resource);

				return cmdmdl;
			};

		#endregion




		public CommandModel<ReactiveCommand, String> CommandMagnetChop
		{
			get { return _CommandMagnetChopLocator(this).Value; }
			set { _CommandMagnetChopLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandMagnetChop Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandMagnetChop = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandMagnetChopLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandMagnetChopLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandMagnetChop), model => model.Initialize(nameof(CommandMagnetChop), ref model._CommandMagnetChop, ref _CommandMagnetChopLocator, _CommandMagnetChopDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandMagnetChopDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandMagnetChop);           // Command resource  
				var commandId = nameof(CommandMagnetChop);
				var vm = CastToCurrentType(model);
				Func<bool> canexec = () =>
				 {
					 var text = vm.Text;
					 string value;

					 bool rval;
					 rval = TryLocateXnInMagnetUri(text, out value);
					 return rval;
				 };

				var cmd = new ReactiveCommand(canExecute: canexec()) { ViewModel = model }; //New Command Core
				cmd.Do(
						 e =>
						{
							//Todo: Add MagnetChop logic here, or
							var text = vm.Text;
							string value;
							if (TryLocateXnInMagnetUri(text, out value))
							{
								vm.Text = value;
							}
						})
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				var cmdmdl = cmd.CreateCommandModel(resource);
				cmd.ListenCanExecuteObservable(
					vm
					.ListenChanged(x => x.Text)
					.Select(_ => canexec()));


				return cmdmdl;
			};


		#endregion



		public CommandModel<ReactiveCommand, String> CommandMagnetRecovery
		{
			get { return _CommandMagnetRecoveryLocator(this).Value; }
			set { _CommandMagnetRecoveryLocator(this).SetValueAndTryNotify(value); }
		}

		public static Func<BindableBase, CommandModel<ReactiveCommand, string>> CommandMagnetRecoveryDefaultValueFactory
		{
			get
			{
				return _CommandMagnetRecoveryDefaultValueFactory;
			}

			set
			{
				_CommandMagnetRecoveryDefaultValueFactory = value;
			}
		}
		#region Property CommandModel<ReactiveCommand, String> CommandMagnetRecovery Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandMagnetRecovery = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandMagnetRecoveryLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandMagnetRecoveryLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandMagnetRecovery), model => model.Initialize(nameof(CommandMagnetRecovery), ref model._CommandMagnetRecovery, ref _CommandMagnetRecoveryLocator, CommandMagnetRecoveryDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandMagnetRecoveryDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandMagnetRecovery);           // Command resource  
				var commandId = nameof(CommandMagnetRecovery);
				var vm = CastToCurrentType(model);
				Func<bool> canexec =
					() =>
					{
						var rval = false;
						var text = vm.Text.Trim();
						string output;
						bool r1;
						rval = TryRecoveryMagnetUri(text, out output);
						return rval;
					};

				var cmd = new ReactiveCommand(canExecute: canexec()) { ViewModel = model }; //New Command Core

				cmd.Do(e =>
						{

							var text = vm.Text.Trim();
							string url = null;
							if (TryRecoveryMagnetUri(text, out url))
							{
								vm.Text = url;
							}
							//Todo: Add MagnetRecovery logic here, or
						})
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				var cmdmdl = cmd.CreateCommandModel(resource);

				cmd.ListenCanExecuteObservable(vm
				  .ListenChanged(x => x.Text)
				  .Select(x => canexec()));
				return cmdmdl;
			};

		private static bool TryRecoveryMagnetUri(string text, out string output)
		{
			output = null;
			var r1 = false;
			var core = text.Split('&').FirstOrDefault().ToLower();
			var skip = 0;
			var left = "urn:btih:";
			var hashead = core.StartsWith(left);
			if (hashead)
			{
				skip = left.Length;
			}
			if (core.Skip(skip).All(c => _hexChars.Contains(c)))
			{

				output = string.Format("magnet:?xt={0}{1}", hashead ? "" : left, text);
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion


		static ISet<char> _hexChars = new HashSet<char>(
			  "abcdefABCDEF01234567890"
			);
		private static bool TryLocateXnInMagnetUri(string text, out string value)
		{
			var left = "urn:btih:";
			value = null;
			Uri target;
			if (Uri.TryCreate(text, UriKind.Absolute, out target))
			{
				if (string.Equals(target.Scheme, "magnet", StringComparison.CurrentCultureIgnoreCase))
				{
					var querys = target.Query
						.Split('?')
						.Skip(1)
						.FirstOrDefault()?
						.Split('&')
						.Select(s => s.Split('='))
						.Select(sa => new
						{
							key = sa[0].ToLower(),
							value = sa.Skip(1).FirstOrDefault()?.ToLower()
						})
						.Where(kv => string.Equals("xt", kv.key, StringComparison.CurrentCultureIgnoreCase))
						.ToDictionary(kv => kv.key, kv => kv.value);

					if (querys.TryGetValue("xt", out value))
					{
						if (value.StartsWith(left))
						{

							value = value.Remove(0, left.Length);
							if (value.All(c => _hexChars.Contains(c)))
							{
								return true;
							}
						}

					}
				}
			}

			value = null;
			return false;
		}


		public CommandModel<ReactiveCommand, String> CommandConvertToWebUri
		{
			get { return _CommandConvertToWebUriLocator(this).Value; }
			set { _CommandConvertToWebUriLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property CommandModel<ReactiveCommand, String> CommandConvertToWebUri Setup        

		protected Property<CommandModel<ReactiveCommand, String>> _CommandConvertToWebUri = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandConvertToWebUriLocator };
		static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandConvertToWebUriLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>(nameof(CommandConvertToWebUri), model => model.Initialize(nameof(CommandConvertToWebUri), ref model._CommandConvertToWebUri, ref _CommandConvertToWebUriLocator, _CommandConvertToWebUriDefaultValueFactory));
		static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandConvertToWebUriDefaultValueFactory =
			model =>
			{
				var resource = nameof(CommandConvertToWebUri);           // Command resource  
				var commandId = nameof(CommandConvertToWebUri);
				var vm = CastToCurrentType(model);
				var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

				cmd.Do(async e =>
				{

					var msb = new MessageDialog("What's next?", "The Text is a well formed uri.");
					var commandOpenLink = new UICommand(
						"Open Link",
						async c =>
						{
							await Launcher.LaunchUriAsync(new Uri(vm.Text));
						});

					var commandCovertIt = new UICommand("Covert To Weblink",
						c =>
						{
							EventRouter.Instance.RaiseEvent<Tuple<EventMessage, Object>>(
							vm,
							Tuple.Create<EventMessage, Object>(EventMessage.ConvertToWebUri, vm.Text)
							);

						});
					var commandCancelIt = new UICommand("Leave");
					msb.Commands.Add(commandOpenLink);
					msb.Commands.Add(commandCovertIt);
					msb.Commands.Add(commandCancelIt);
					msb.CancelCommandIndex = 2;
					msb.DefaultCommandIndex = 1;
					var selected = await msb.ShowAsync();


				})
					.DoNotifyDefaultEventRouter(vm, commandId)
					.Subscribe()
					.DisposeWith(vm);

				var cmdmdl = cmd.CreateCommandModel(resource);


				return cmdmdl;
			};

		#endregion


	}
}
