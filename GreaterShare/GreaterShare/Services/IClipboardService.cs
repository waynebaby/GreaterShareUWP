using GreaterShare.Models;
using GreaterShare.Models.ClipboardData;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace GreaterShare.Services
{
	public interface IClipboardService : INotifyPropertyChanged
	{
		event EventHandler<ClipboardContent> ClipboardContentChanged;
		ClipboardContent CurrentClipboardContent { get; }
	}



}
