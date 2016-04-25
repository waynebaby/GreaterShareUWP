using Microsoft.Graphics.Canvas;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using MVVMSidekick.Services;
using GreaterShare.Services;
using Windows.Foundation;
using MVVMSidekick.EventRouting;
using Windows.UI.Xaml.Data;
using Windows.ApplicationModel.Resources;

namespace GreaterShare.Glue
{
    public class InkCanvasControllerBehavior : Behavior<InkCanvas>
    {

        protected override void OnAttached()
        {
            base.OnAttached();

            try
            {

                RegisterPropertyChangedAndSaveUnregToken(
                   IsInputEnabledProperty,
                   (o, a) => this.InkPresenter.IsInputEnabled = IsInputEnabled);

                RegisterPropertyChangedAndSaveUnregToken(
                    InputDeviceTypesProperty,
                    (o, a) => this.InkPresenter.InputDeviceTypes = InputDeviceTypes);

                DependencyPropertyChangedCallback detailChanged = (o, a) =>
                 {
                     InkPresenter.InputProcessingConfiguration.Mode = CurrentProfile == InkCanvasProfileType.Erasing ? InkInputProcessingMode.Erasing : InkInputProcessingMode.Inking;
                     var att = InkPresenter.CopyDefaultDrawingAttributes();
                     att.Color = Color;
                     att.PenTip = PenShape;
                     att.IgnorePressure = false;
                     att.DrawAsHighlighter = CurrentProfile == InkCanvasProfileType.Highlighting;
                     att.Size = StrokeSize;
                     InkPresenter.UpdateDefaultDrawingAttributes(att);

                 };

                RegisterPropertyChangedAndSaveUnregToken(ColorProperty, detailChanged);
                RegisterPropertyChangedAndSaveUnregToken(StrokeSizeProperty, detailChanged);
                RegisterPropertyChangedAndSaveUnregToken(CurrentProfileProperty, (o, a) =>
                {
                    switch (CurrentProfile)
                    {
                        case InkCanvasProfileType.Drawing:
                            ProfileOfDrawing?.ApplyToInkCanvasController(this);
                            break;
                        case InkCanvasProfileType.Highlighting:
                            ProfileOfHighlighting?.ApplyToInkCanvasController(this);
                            break;
                        case InkCanvasProfileType.Erasing:
                            ProfileOfErasing?.ApplyToInkCanvasController(this);
                            break;
                        default:
                            break;
                    }
                    detailChanged(o, a);
                });
                RegisterPropertyChangedAndSaveUnregToken(PenShapeProperty, detailChanged);


                regs.ForEach(i => i.Item3(null, null));



            }
            catch (Exception ex)
            {

                EventRouter.Instance.RaiseEvent(this, ex);
            }


        }

        private InkPresenter InkPresenter
        {
            get { return AssociatedObject.InkPresenter; }
        }

        private void RegisterPropertyChangedAndSaveUnregToken(DependencyProperty dp, DependencyPropertyChangedCallback callback)
        {
            var regv = RegisterPropertyChangedCallback(dp, callback);
            regs.Add(Tuple.Create(dp, regv, callback));
        }

        List<Tuple<DependencyProperty, long, DependencyPropertyChangedCallback>> regs = new List<Tuple<DependencyProperty, long, DependencyPropertyChangedCallback>>();






        protected override void OnDetaching()
        {
            regs.ForEach(i => UnregisterPropertyChangedCallback(i.Item1, i.Item2));

            regs.Clear();

            base.OnDetaching();

        }



        public Size StrokeSize
        {
            get { return (Size)GetValue(StrokeSizeProperty); }
            set { SetValue(StrokeSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeSizeProperty =
            DependencyProperty.Register(nameof(StrokeSize), typeof(Size), typeof(InkCanvasControllerBehavior), new PropertyMetadata(new Size(2, 2)));



        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(nameof(Color), typeof(Color), typeof(InkCanvasControllerBehavior), new PropertyMetadata(Colors.Red));





        public InkCanvasProfileType CurrentProfile
        {
            get { return (InkCanvasProfileType)GetValue(CurrentProfileProperty); }
            set { SetValue(CurrentProfileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentProfile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentProfileProperty =
            DependencyProperty.Register(nameof(CurrentProfile), typeof(InkCanvasProfileType), typeof(InkCanvasControllerBehavior), new PropertyMetadata(InkCanvasProfileType.Drawing));





        public bool IsInputEnabled
        {
            get { return (bool)GetValue(IsInputEnabledProperty); }
            set { SetValue(IsInputEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsInputEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsInputEnabledProperty =
            DependencyProperty.Register(nameof(IsInputEnabled), typeof(bool), typeof(InkCanvasControllerBehavior), new PropertyMetadata(false));




        public CoreInputDeviceTypes InputDeviceTypes
        {
            get { return (CoreInputDeviceTypes)GetValue(InputDeviceTypesProperty); }
            set { SetValue(InputDeviceTypesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InputDeviceTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputDeviceTypesProperty =
            DependencyProperty.Register(nameof(InputDeviceTypes), typeof(CoreInputDeviceTypes), typeof(InkCanvasControllerBehavior), new PropertyMetadata(
                    CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Touch
                ));


        public string OutputBase64String
        {
            get { return (string)GetValue(OutputBase64StringProperty); }
            set { SetValue(OutputBase64StringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OutputBase64String.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutputBase64StringProperty =
            DependencyProperty.Register(nameof(OutputBase64String), typeof(string), typeof(InkCanvasControllerBehavior), new PropertyMetadata(null));

        public PenTipShape PenShape
        {
            get { return (PenTipShape)GetValue(PenShapeProperty); }
            set { SetValue(PenShapeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PenShape.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PenShapeProperty =
            DependencyProperty.Register(nameof(PenTipShape), typeof(PenTipShape), typeof(InkCanvasControllerBehavior), new PropertyMetadata(PenTipShape.Circle));




        public string BackgroundImageBase64String
        {
            get { return (string)GetValue(BackgroundImageBase64StringProperty); }
            set { SetValue(BackgroundImageBase64StringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundImageBase64String.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundImageBase64StringProperty =
            DependencyProperty.Register(nameof(BackgroundImageBase64String), typeof(string), typeof(InkCanvasControllerBehavior), new PropertyMetadata(null));



        public void RenderImageToOutput()
        {
            var strokes = AssociatedObject.InkPresenter.StrokeContainer.GetStrokes();
            var width = (int)AssociatedObject.ActualWidth;
            var height = (int)AssociatedObject.ActualHeight;
            byte[] backgroundImageBuffer = null;
            if (BackgroundImageBase64String != null)
            {
                backgroundImageBuffer = Convert.FromBase64String(BackgroundImageBase64String);
            }

            byte[] outputBuffer = ServiceLocator.Instance
                .Resolve<IDrawingService>().DrawStrokeOnImageBackground(strokes, backgroundImageBuffer);

            OutputBase64String = Convert.ToBase64String(outputBuffer);

            if (ImageRendered != null)
            {
                ImageRendered(this, EventArgs.Empty);
            }

        }


        public event EventHandler<EventArgs> ImageRendered;




        public InkCanvasControllerProfile ProfileOfDrawing
        {
            get { return (InkCanvasControllerProfile)GetValue(ProfileOfDrawingProperty); }
            set { SetValue(ProfileOfDrawingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProfileOfDrawing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProfileOfDrawingProperty =
            DependencyProperty.Register(nameof(ProfileOfDrawing), typeof(InkCanvasControllerProfile), typeof(InkCanvasControllerBehavior), new PropertyMetadata(new InkCanvasControllerProfile
            {
                PreferedPenTipShape = PenTipShape.Circle,
                PreferedStrokeColor = Colors.Red,
                PreferedStrokeSize = new Size(2, 2)
            }));



        public InkCanvasControllerProfile ProfileOfHighlighting
        {
            get { return (InkCanvasControllerProfile)GetValue(ProfileOfHighlightingProperty); }
            set { SetValue(ProfileOfHighlightingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProfileOfHighlighting.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProfileOfHighlightingProperty =
            DependencyProperty.Register(nameof(ProfileOfHighlighting), typeof(InkCanvasControllerProfile), typeof(InkCanvasControllerBehavior), new PropertyMetadata(new InkCanvasControllerProfile
            {
                PreferedPenTipShape = PenTipShape.Rectangle,
                PreferedStrokeColor = Colors.Yellow,
                PreferedStrokeSize = new Size(16, 16)
            }));


        public InkCanvasControllerProfile ProfileOfErasing
        {
            get { return (InkCanvasControllerProfile)GetValue(ProfileOfErasingProperty); }
            set { SetValue(ProfileOfErasingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProfileOfErasing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProfileOfErasingProperty =
            DependencyProperty.Register(nameof(ProfileOfErasing), typeof(InkCanvasControllerProfile), typeof(InkCanvasControllerBehavior), new PropertyMetadata(new InkCanvasControllerProfile
            {
                PreferedPenTipShape = PenTipShape.Rectangle,
                PreferedStrokeColor = Colors.Yellow,
                PreferedStrokeSize = new Size(4, 4)
            }));

    }

    public enum InkCanvasProfileType
    {
        Drawing,
        Highlighting,
        Erasing
    }

    public class InkCanvasProfileTypeLocalizedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is InkCanvasProfileType)
            {
                var v = (InkCanvasProfileType)value;
                var n = v.ToString();
                return ResourceLoader.GetForViewIndependentUse().GetString(nameof(InkCanvasProfileType) + "_" + n);

            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    public class InkCanvasProfileItem : DependencyObject
    {


        public InkCanvasProfileType Type
        {
            get { return (InkCanvasProfileType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register(nameof(Type), typeof(InkCanvasProfileType), typeof(InkCanvasProfileItem), new PropertyMetadata(0));


        public String Glyph
        {
            get { return (String)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Glyph.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.Register(nameof(Glyph), typeof(String), typeof(InkCanvasProfileItem), new PropertyMetadata(null));




    }

    public class InkCanvasControllerProfile : DependencyObject
    {


        public Size PreferedStrokeSize
        {
            get { return (Size)GetValue(PreferedStrokeSizeProperty); }
            set { SetValue(PreferedStrokeSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreferedStrokeSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreferedStrokeSizeProperty =
            DependencyProperty.Register(nameof(PreferedStrokeSize), typeof(Size), typeof(InkCanvasControllerProfile), new PropertyMetadata(new Size(2, 2)));




        public Color PreferedStrokeColor
        {
            get { return (Color)GetValue(PreferedStrokeColorProperty); }
            set { SetValue(PreferedStrokeColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreferedStrokeColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreferedStrokeColorProperty =
            DependencyProperty.Register(nameof(PreferedStrokeColor), typeof(Color), typeof(InkCanvasControllerProfile), new PropertyMetadata(Colors.Red));



        public PenTipShape PreferedPenTipShape
        {
            get { return (PenTipShape)GetValue(PreferedPenTipShapeProperty); }
            set { SetValue(PreferedPenTipShapeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreferedPenTipShape.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreferedPenTipShapeProperty =
            DependencyProperty.Register(nameof(PreferedPenTipShape), typeof(PenTipShape), typeof(InkCanvasControllerProfile), new PropertyMetadata(PenTipShape.Circle));


        public void ApplyToInkCanvasController(InkCanvasControllerBehavior controller)
        {
            controller.Color = PreferedStrokeColor;
            controller.StrokeSize = PreferedStrokeSize;
            controller.PenShape = PreferedPenTipShape;
        }

    }

}
