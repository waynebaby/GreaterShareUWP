using MVVMSidekick.Common;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace GreaterShare.Models
{
	[DataContract]
	public class MemoryStreamBase64Item : BindableBase<MemoryStreamBase64Item>
	{

		public MemoryStreamBase64Item()
		{
		}

		public MemoryStreamBase64Item(byte[] bytes)
		{
			Base64String = Convert.ToBase64String(bytes);
		}


		public async Task<InMemoryRandomAccessStream> GetRandowmAccessStreamAsync()
		{
			InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
			var b = Convert.FromBase64String(Base64String);
			await stream.WriteAsync(b.AsBuffer());
			stream.Seek(0);
			return stream;
		}
		public InMemoryRandomAccessStream GetRandowmAccessStream()
		{
			return GetRandowmAccessStreamAsync().Result;
		}


		[DataMember]
		public string Base64String
		{
			get { return _Base64StringLocator(this).Value; }
			set { _Base64StringLocator(this).SetValueAndTryNotify(value); }
		}
		#region Property string Base64String Setup        
		protected Property<string> _Base64String = new Property<string> { LocatorFunc = _Base64StringLocator };
		static Func<BindableBase, ValueContainer<string>> _Base64StringLocator = RegisterContainerLocator<string>("Base64String", model => model.Initialize("Base64String", ref model._Base64String, ref _Base64StringLocator, _Base64StringDefaultValueFactory));
		static Func<string> _Base64StringDefaultValueFactory = () => default(string);
		#endregion



	}
}
