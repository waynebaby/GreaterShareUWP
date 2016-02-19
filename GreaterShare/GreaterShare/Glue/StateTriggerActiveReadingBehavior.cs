using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace GreaterShare.Glue
{
	public class StateTriggerActiveReadingBehavior : Behavior<Panel>
	{

		protected override void OnAttached()
		{
			AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;

			base.OnAttached();
		}

		private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			var isInNarrowMode = AssociatedObject.ActualWidth < AssociatedObject.ActualHeight;
			var isInWideMode = !isInNarrowMode;
			if (NarrowTrigger!=null && isInNarrowMode && (!NarrowTrigger.IsActive))
			{
				WideTrigger.IsActive = false;
				NarrowTrigger.IsActive = true;
			}
			if (WideTrigger!=null&& isInWideMode &&(!WideTrigger.IsActive))
			{
				NarrowTrigger.IsActive = false;
				WideTrigger.IsActive = true;
			}

		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.SizeChanged -= AssociatedObject_SizeChanged;

		}




		public StateTrigger NarrowTrigger
		{
			get { return (StateTrigger)GetValue(NarrowTriggerProperty); }
			set { SetValue(NarrowTriggerProperty, value); }
		}

		// Using a DependencyProperty as the backing store for NarrowTrigger.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty NarrowTriggerProperty =
			DependencyProperty.Register("NarrowTrigger", typeof(StateTrigger), typeof(StateTriggerActiveReadingBehavior), new PropertyMetadata(null));



		public StateTrigger WideTrigger
		{
			get { return (StateTrigger)GetValue(WideTriggerProperty); }
			set { SetValue(WideTriggerProperty, value); }
		}

		// Using a DependencyProperty as the backing store for WideTrigger.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty WideTriggerProperty =
			DependencyProperty.Register("WideTrigger", typeof(StateTrigger), typeof(StateTriggerActiveReadingBehavior), new PropertyMetadata(null));



		//public bool IsInWideMode
		//{
		//	get { return (bool)GetValue(IsInWideModeProperty); }
		//	set { SetValue(IsInWideModeProperty, value); }
		//}

		//// Using a DependencyProperty as the backing store for IsInWideMode.  This enables animation, styling, binding, etc...
		//public static readonly DependencyProperty IsInWideModeProperty =
		//	DependencyProperty.Register("IsInWideMode", typeof(bool), typeof(StateTriggerActiveReadingBehavior), new PropertyMetadata(false));



		//public bool IsInNarrowMode
		//{
		//	get { return (bool)GetValue(IsInNarrowModeProperty); }
		//	set { SetValue(IsInNarrowModeProperty, value); }
		//}

		//// Using a DependencyProperty as the backing store for IsInNarrowMode.  This enables animation, styling, binding, etc...
		//public static readonly DependencyProperty IsInNarrowModeProperty =
		//	DependencyProperty.Register("IsInNarrowMode", typeof(bool), typeof(bool), new PropertyMetadata(true ));




		//public bool ForWideView { get; set; }

		//public object ConvertBack(object value, Type targetType, object parameter, string language)
		//{
		//	throw new NotImplementedException();
		//}
	}
}
