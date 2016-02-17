using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GreaterShare.Glue
{
	public class FalseIfIsStringNullOrEmptyConverter : IValueConverter	
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var s = value?.ToString();
			return !string.IsNullOrEmpty(s);

		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
