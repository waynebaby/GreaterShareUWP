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
	public class HideWhenIsNotEditingConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var mainPage = ((Frame)Window.Current.Content).Content as MainPage;

			var isEditing = mainPage?.StrongTypeViewModel.FocusingViewIndex == 0;


			return
				  !(isEditing)
				? Windows.UI.Xaml.Visibility.Collapsed : Windows.UI.Xaml.Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
