using GreaterShare.ViewModels;
using MVVMSidekick.EventRouting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreaterShare.Glue
{
	public class AddTextCommentsCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			EventRouter.Instance.RaiseEvent<Tuple<EventMessage, Object>>(
				this,
				Tuple.Create<EventMessage, Object>(EventMessage.AddTextComment, null)
				);
		}
	}
}
