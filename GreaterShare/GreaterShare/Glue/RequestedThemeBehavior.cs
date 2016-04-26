using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GreaterShare.Glue
{
    public class RequestedThemeBehavior : Behavior<FrameworkElement>
    {

        public RequestedThemeBehavior()
        {
            using (Disposable.Create(() => _BlockingBroadcast = false))
            {
                _BlockingBroadcast = true;

                IsInLightTheme = _AssociatedObjectsRequestedTheme == ElementTheme.Light;
            }
        }

        private async void RequestedThemeBehavior_RequestedThemeChanged(object sender, ElementTheme e)
        {
            if (IsValueGlobalEffective)
            {

                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High,
                    () =>
                    {
                        using (Disposable.Create(() => _BlockingBroadcast = false))
                        {
                            _BlockingBroadcast = true;

                            IsInLightTheme =
                                AssociatedObject.RequestedTheme == ElementTheme.Light ||
                                (Application.Current.RequestedTheme == ApplicationTheme.Light && AssociatedObject.RequestedTheme == ElementTheme.Default);

                        }
                    });
                
            }

        }


        protected override void OnAttached()
        {

            RequestedThemeChanged += RequestedThemeBehavior_RequestedThemeChanged;
            AssociatedObject.RequestedTheme = _AssociatedObjectsRequestedTheme;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            RequestedThemeChanged -= RequestedThemeBehavior_RequestedThemeChanged;
        }

        private bool _BlockingBroadcast = false;
        static ElementTheme _AssociatedObjectsRequestedTheme = Application.Current.RequestedTheme == ApplicationTheme.Light ? ElementTheme.Light : ElementTheme.Dark;

        private static ElementTheme AssociatedObjectsRequestedTheme
        {
            get
            {
                return _AssociatedObjectsRequestedTheme;
            }

            set
            {

                _AssociatedObjectsRequestedTheme = value;
                RequestedThemeChanged?.Invoke(typeof(RequestedThemeBehavior), value);

            }
        }


        private static event EventHandler<ElementTheme> RequestedThemeChanged;


        public bool IsInLightTheme
        {
            get { return (bool)GetValue(IsInLightThemeProperty); }
            set { SetValue(IsInLightThemeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsInLightTheme.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsInLightThemeProperty =
            DependencyProperty.Register(nameof (IsInLightTheme), typeof(bool), typeof(RequestedThemeBehavior), new PropertyMetadata(false, OnNewValue));


        public bool IsValueGlobalEffective
        {
            get { return (bool)GetValue(IsValueGlobalEffectiveProperty); }
            set { SetValue(IsValueGlobalEffectiveProperty, value); }
        }



        // Using a DependencyProperty as the backing store for IsValueGlobalEffective.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsValueGlobalEffectiveProperty =
            DependencyProperty.Register(nameof(IsValueGlobalEffective), typeof(bool), typeof(RequestedThemeBehavior), new PropertyMetadata(false));



        private static void OnNewValue(DependencyObject o, DependencyPropertyChangedEventArgs a)
        {
            if (a.NewValue is bool?)
            {
                var beh = (o as RequestedThemeBehavior);
                var target = beh.AssociatedObject;
                if (beh != null && target!=null)
                {

                    var v = (bool?)a.NewValue;

                    switch (v)
                    {
                        case true:
                            target.RequestedTheme = ElementTheme.Light;
                            break;
                        case false:
                            target.RequestedTheme = ElementTheme.Dark;
                            break;
                        case null:
                        default:
                            target.RequestedTheme = ElementTheme.Default;
                            break;
                    }
                    if (beh.IsValueGlobalEffective && (!beh._BlockingBroadcast))
                    {
                        AssociatedObjectsRequestedTheme = target.RequestedTheme;
                    }

                }

            }

        }


    }
}
