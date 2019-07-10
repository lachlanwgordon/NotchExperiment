using System;
using System.Linq;
using Android.Views;
using Android.Widget;
using NotchExperiment.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Views.View;
using static NotchExperiment.Device;

[assembly: ResolutionGroupName("NotchExperiment")]
[assembly: ExportEffect(typeof(NotchEffect), nameof(NotchEffect))]
namespace NotchExperiment.Droid
{
    public class NotchEffect : PlatformEffect
    {
        public LinearLayout NotchLayout { get; private set; }
        public Device Device { get; private set; }
        public Android.Views.View.IOnLayoutChangeListener UpdateLayout { get; private set; }
        NotchExperiment.NotchEffect NotchInfo;

        protected override void OnAttached()
        {
            NotchInfo = Element.Effects.FirstOrDefault(x => x is NotchExperiment.NotchEffect) as NotchExperiment.NotchEffect;

            if (Element == null)
            {
                NotchLayout = null;
            }
            else if (NotchLayout == null)
            {
                InitView();
            }
        }

        protected override void OnDetached()
        {
        }


        void InitView()
        {
            NotchLayout = new LinearLayout(Container.Context);
            NotchLayout.LayoutParameters = new Android.Views.ViewGroup.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.MatchParent);


            var button = new Android.Widget.Button(Container.Context);
            button.Text = "Notch";

            var page = Element as Page;

            if (NotchInfo.Model == PhoneModels.Custom)
            {
                Device = NotchInfo.CustomDevice;
            }
            else
            {
                Device = Devices[NotchInfo.Model];
            }


            var notch = new NotchView(Container.Context) { Device = Device };

            NotchLayout.AddView(notch);
            Container.AddView(NotchLayout);

            NotchLayout.Parent.BringChildToFront(NotchLayout);


            Container.LayoutChange += (sender, e) =>
            {
                if (NotchLayout != null)
                {
                    var msw = MeasureSpec.MakeMeasureSpec(e.Right , MeasureSpecMode.Exactly);
                    var msh = MeasureSpec.MakeMeasureSpec(e.Bottom, MeasureSpecMode.Exactly);

                    NotchLayout.Measure(msw, msh);
                    NotchLayout.Layout(0, 0, e.Right , e.Bottom);

                    NotchLayout.Parent.BringChildToFront(NotchLayout);

                }

            };
        }

        
    }
}
