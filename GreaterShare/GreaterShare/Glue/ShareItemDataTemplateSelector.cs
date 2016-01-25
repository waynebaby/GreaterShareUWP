using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GreaterShare.Glue
{
	public class ShareItemDataTemplateSelector : DataTemplateSelector
	{


		protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
		{				   

			var key = item?.GetType().Name;
			key = key == null ? "default" : PrefixOfTemplateKey + key;

			object dt = null;

			var keyDicts = App.Current.Resources;
			var got = keyDicts?.TryGetValue(key, out dt);

			if (got != null && got.Value)
			{
				return dt as DataTemplate;
			}
			else
			{
				return base.SelectTemplateCore(item);
			}

		}
		public string PrefixOfTemplateKey { get; set; } = "";
	}
}
