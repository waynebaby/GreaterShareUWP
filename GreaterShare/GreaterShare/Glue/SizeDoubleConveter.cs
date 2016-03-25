using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace GreaterShare.Glue
{
	public class SizeDoubleConveter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if (value is Size)
			{
				var s = (Size)value;
				return s.Height;
			}
			else if (value is Double)
			{
				var w = (Double)value;
				return new Size(w, w);
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			if (value is Size)
			{
				var s = (Size)value;
				return s.Height;
			}
			else if (value is Double)
			{
				var w = (Double)value;
				return new Size(w, w);
			}
			return value;
		}
	}
}
