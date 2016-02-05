using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace GreaterShare.Glue
{
	public class HideWhenNotMatchConverter : DependencyObject, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return value?.ToString() == parameter?.ToString() ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}

		


	}
}
