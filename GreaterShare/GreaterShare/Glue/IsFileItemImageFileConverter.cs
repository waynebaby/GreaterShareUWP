using GreaterShare.Models.Sharing.ShareItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GreaterShare.Glue
{
	public class IsFileItemImageFileConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			FileItem file = null;
			if ((file=value as FileItem)==null )
			{
				return false ;
			}

			return file.ContentType?.StartsWith("image/") ?? false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
