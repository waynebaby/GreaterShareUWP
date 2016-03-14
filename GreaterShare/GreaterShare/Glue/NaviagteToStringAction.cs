using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GreaterShare.Glue
{
	public class NaviagteToStringAction : DependencyObject, IAction
	{

																	  

		public string NaviagteHtmlString
		{
			get { return (string)GetValue(NaviagteHtmlStringProperty); }
			set { SetValue(NaviagteHtmlStringProperty, value); }
		}

		// Using a DependencyProperty as the backing store for NaviagteHtmlString.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty NaviagteHtmlStringProperty =
			DependencyProperty.Register("NaviagteHtmlString", typeof(string), typeof(NaviagteToStringAction), new PropertyMetadata(0));




		public object Execute(object sender, object parameter)
		{
			if (sender != null)
			{	 
				(sender as WebView)?.NavigateToString(NaviagteHtmlString ?? "<html/>");
			}
			return parameter;
		}
	}
}
