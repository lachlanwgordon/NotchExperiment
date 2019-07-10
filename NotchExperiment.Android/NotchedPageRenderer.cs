using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using Android.Views;
using Android.Widget;
using NotchExperiment;
using NotchExperiment.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static NotchExperiment.Device;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(NotchedPage), typeof(NotchedPageRenderer))]
namespace NotchExperiment.Droid
{
    public class NotchedPageRenderer : PageRenderer
    {
        public LinearLayout NotchLayout { get; private set; }
        public Device device { get; private set; }

        public NotchedPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
            {
                NotchLayout = null;
            }
            else if (NotchLayout == null)
            {
                InitView();
            }
        }


        void InitView()
        {
            NotchLayout = new LinearLayout(Context);
            NotchLayout.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

            var button = new Android.Widget.Button(Context);
            button.Text = "Notch";



            var notchedPage = Element as NotchedPage;

            if (notchedPage.Model == PhoneModels.Custom)
            {
                device = notchedPage.CustomDevice;
            }
            else
            {
                device = Devices[notchedPage.Model];
            }
            Element.Padding = new Thickness(Element.Padding.Left, Element.Padding.Top + device.StatusBarHeight, Element.Padding.Right, Element.Padding.Bottom);


            var notch = new NotchView(Context) { Device = device };

            NotchLayout.AddView(notch);
            AddView(NotchLayout);

            NotchLayout.Parent.BringChildToFront(NotchLayout);

            //NotchLayout.SetBackgroundColor(Color.ForestGreen);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (NotchLayout != null)
            {
                var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
                var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

                NotchLayout.Measure(msw, msh);
                NotchLayout.Layout(0, 0, r - l, b - t);

                NotchLayout.Parent.BringChildToFront(NotchLayout);

            }
        }

        //protected override bool DrawChild(Canvas canvas, Android.Views.View child, long drawingTime)
        //{
        //    var paint = new Paint
        //    {
        //        Color = Color.Gray,
        //    };


        //    var height = device.NotchHeight - device.StatusBarHeight  ;
        //    canvas.DrawRect(0, 0, canvas.Width, height, paint);

        //    return base.DrawChild(canvas, child, drawingTime);
        //}



    }

   
}
