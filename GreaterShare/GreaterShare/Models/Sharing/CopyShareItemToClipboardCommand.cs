using GreaterShare.Services;
using MVVMSidekick.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreaterShare.Models.Sharing
{
	public class CopyShareItemToClipboardCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public async void Execute(object parameter)
		{
			var svc = ServiceLocator.Instance.Resolve<IShareService>();
			await svc.SetToClipboardAsync(parameter);
		}
	}
}
