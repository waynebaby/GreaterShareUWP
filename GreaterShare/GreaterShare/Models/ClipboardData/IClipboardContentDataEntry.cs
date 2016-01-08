using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.Models.ClipboardData
{


	public interface IClipboardContentDataEntry
	{
		string FormatName { get; }									
		object EntryObject { get; }
	}
}
