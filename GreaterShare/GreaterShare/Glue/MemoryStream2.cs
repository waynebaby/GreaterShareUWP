using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.Glue
{
	public class MemoryStream2 : MemoryStream
	{

		int _WriteTimeout;

		public override int WriteTimeout
		{
			get
			{
				return _WriteTimeout;
			}

			set
			{
				_WriteTimeout = value;
			}
		}
		int _ReadTimeout;
		public override int ReadTimeout
		{
			get
			{
				return _ReadTimeout;

			}

			set
			{
				_ReadTimeout = value;
			}
		}
	}
}
