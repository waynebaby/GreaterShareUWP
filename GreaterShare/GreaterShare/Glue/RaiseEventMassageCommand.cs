using GreaterShare.ViewModels;
using MVVMSidekick.EventRouting;
using MVVMSidekick.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace GreaterShare.Glue
{
	public class RaiseEventMassageCommand : DependencyObject, ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			EventRouter.Instance.RaiseEvent(this, Tuple.Create(EventMessage, parameter));
		}



		public EventMessage EventMessage
		{
			get { return (EventMessage)GetValue(EventMessageProperty); }
			set { SetValue(EventMessageProperty, value); }
		}

		// Using a DependencyProperty as the backing store for EventMessage.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty EventMessageProperty =
			DependencyProperty.Register("EventMessage", typeof(EventMessage), typeof(RaiseEventMassageCommand), new PropertyMetadata(0));





		//public Object MessageParameter
		//{
		//	get { return (Object)GetValue(MessageParameterProperty); }
		//	set { SetValue(MessageParameterProperty, value); }
		//}

		//// Using a DependencyProperty as the backing store for MessageParameter.  This enables animation, styling, binding, etc...
		//public static readonly DependencyProperty MessageParameterProperty =
		//	DependencyProperty.Register("MessageParameter", typeof(Object), typeof(RaiseEventMassageCommand), new PropertyMetadata(0));


	}
}
