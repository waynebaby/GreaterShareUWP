using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace GreaterShare.Glue
{
	public class IsStoryboardPlayingBehavior : Behavior<FrameworkElement>
	{
		protected override void OnAttached()
		{
			base.OnAttached();

			regid = AssociatedObject.RegisterPropertyChangedCallback(IsGuidingToProperty,
				(o, a) =>
				{
					var g = o as Grid;

					if (Storyboard != null)
					{
						ControlStoryboard(Storyboard);
					}
					else if (g.Resources.ContainsKey(StoryboardResourceKey))
					{
						var sb = g.Resources[StoryboardResourceKey] as Storyboard;
						ControlStoryboard(sb);

					}

				});

		}

		private void ControlStoryboard(Storyboard sb)
		{
			if (this.IsPlaying)
			{
				sb?.Begin();
			}
			else
			{
				sb?.Stop();
			}
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.UnregisterPropertyChangedCallback(IsGuidingToProperty, regid);
		}
		long regid = 0;




		public string StoryboardResourceKey
		{
			get { return (string)GetValue(AniKeyProperty); }
			set { SetValue(AniKeyProperty, value); }
		}

		// Using a DependencyProperty as the backing store for AniKey.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty AniKeyProperty =
			DependencyProperty.Register(nameof(StoryboardResourceKey), typeof(string), typeof(IsStoryboardPlayingBehavior), new PropertyMetadata("AniKey"));




		public Storyboard Storyboard
		{
			get { return (Storyboard)GetValue(StoryboardProperty); }
			set { SetValue(StoryboardProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Storyboard.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty StoryboardProperty =
			DependencyProperty.Register(nameof(Storyboard), typeof(Storyboard), typeof(IsStoryboardPlayingBehavior), new PropertyMetadata(0));



		public bool IsPlaying
		{
			get { return (bool)GetValue(IsGuidingToProperty); }
			set { SetValue(IsGuidingToProperty, value); }
		}
		// Using a DependencyProperty as the backing store for IsGuidingTo.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsGuidingToProperty =
			DependencyProperty.Register(nameof(IsPlaying), typeof(bool), typeof(IsStoryboardPlayingBehavior), new PropertyMetadata(false));


	}
}
