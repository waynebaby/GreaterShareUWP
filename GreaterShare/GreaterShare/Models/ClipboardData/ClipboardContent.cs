using GreaterShare.Models.ClipboardData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.Models
{

	public class ClipboardContent
	{
		public ClipboardContent(IReadOnlyList<IClipboardContentDataEntry> contentDataEntries, CopyOperation operation)
		{
			ContentDataEntries = contentDataEntries;
			Operation = operation;
			_initFactoryTask = new Lazy<Task<IReadOnlyList<IClipboardContentDataEntry>>>();

		}

		Lazy<Task<IReadOnlyList<IClipboardContentDataEntry>>> _initFactoryTask;

		public async Task InitAsync()
		{
			ContentDataEntries = await _initFactoryTask.Value;
		}

		public IReadOnlyList<IClipboardContentDataEntry> ContentDataEntries { get; private set; }
		public CopyOperation Operation { get; private set; }
	}




	public enum CopyOperation
	{
		None = 0,
		Copy = 1,
		Move = 2,
		Link = 4
	}

}
