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
		long NarrowTriggerPropertyReg;
		long WideTriggerPropertyReg;

		protected override void OnAttached()
		{
			AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;
			NarrowTriggerPropertyReg = RegisterPropertyChangedCallback(NarrowTriggerProperty, (o, a) => RefreshState());
			WideTriggerPropertyReg = RegisterPropertyChangedCallback(WideTriggerProperty, (o, a) => RefreshState());

			base.OnAttached();
		}

		private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			RefreshState();

		}

		private void RefreshState()
		{
			try
			{
				if (AssociatedObject == null)
				{
					return;	   
				}
				var isInNarrowMode = AssociatedObject.ActualWidth < AssociatedObject.ActualHeight;
				var isInWideMode = !isInNarrowMode;
				if (NarrowTrigger != null && isInNarrowMode && (!NarrowTrigger.IsActive))
				{
					if (WideTrigger != null)
					{
						WideTrigger.IsActive = false;

					}
					NarrowTrigger.IsActive = true;
				}
				if (WideTrigger != null && isInWideMode && (!WideTrigger.IsActive))
				{
					if (NarrowTrigger != null)
					{
						NarrowTrigger.IsActive = false;

					}
					WideTrigger.IsActive = true;
				}

			}
			catch (Exception ex)
			{

			}
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.SizeChanged -= AssociatedObject_SizeChanged;
			UnregisterPropertyChangedCallback(NarrowTriggerProperty, NarrowTriggerPropertyReg);
			UnregisterPropertyChangedCallback(WideTriggerProperty, WideTriggerPropertyReg);
		}

   		public StateTrigger NarrowTrigger
		{
			get { return (StateTrigger)GetValue(NarrowTriggerProperty); }
			set { SetValue(NarrowTriggerProperty, value); }
		}

		// Using a DependencyProperty as the backing store for NarrowTrigger.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty NarrowTriggerProperty =
			DependencyProperty.Register(nameof(NarrowTrigger), typeof(StateTrigger), typeof(StateTriggerActiveReadingBehavior), new PropertyMetadata(null));



		public StateTrigger WideTrigger
		{
			get { return (StateTrigger)GetValue(WideTriggerProperty); }
			set { SetValue(WideTriggerProperty, value); }
		}

		// Using a DependencyProperty as the backing store for WideTrigger.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty WideTriggerProperty =
			DependencyProperty.Register(nameof(WideTrigger), typeof(StateTrigger), typeof(StateTriggerActiveReadingBehavior), new PropertyMetadata(null));



	}
}
