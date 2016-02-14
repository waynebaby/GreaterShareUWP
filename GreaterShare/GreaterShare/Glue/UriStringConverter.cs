using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GreaterShare.Glue
{
	public class UriStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{

			return ((value as Uri) ?? new Uri("about:blank")).ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			Uri rval = null;

			if (Uri.TryCreate(value?.ToString(), UriKind.RelativeOrAbsolute, out rval))
			{
				return rval;
			}
			throw new ArgumentException("This string is not a valid Uri.");
		}
	}
}
