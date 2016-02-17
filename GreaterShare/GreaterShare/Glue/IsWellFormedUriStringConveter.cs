using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GreaterShare.Glue
{
	public class IsWellFormedUriStringConveter : IValueConverter	
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var s = value?.ToString();
			return Uri.IsWellFormedUriString(s, UriKind.Absolute);

		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
