using System;
using System.Linq;
using NotchExperiment.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using static NotchExperiment.Device;

[assembly:ResolutionGroupName("NotchExperiment")]
[assembly:ExportEffect(typeof(NotchEffect), nameof(NotchEffect))]
namespace NotchExperiment.iOS
{
    public class NotchEffect : PlatformEffect
    {


        protected override void OnAttached()
        {
            var notchInfo = Element.Effects.FirstOrDefault(x => x is NotchExperiment.NotchEffect) as NotchExperiment.NotchEffect;
            if (notchInfo != null)
            {
                (Element as VisualElement).BackgroundColor = Color.Green;
                Device device;
                if (notchInfo.Model == PhoneModels.Custom)
                {
                    device = notchInfo.CustomDevice;
                }
                else
                {
                    device = Devices[notchInfo.Model];
                }

                NotchView notch;
                if (DesignMode.IsDesignModeEnabled)
                {
                    notch = new NotchView(new System.Drawing.RectangleF(0, 0, (float)device.ScreenWidth, (float)device.ScreenHeight), device);//Previewer always reports size of an iphone 5
                }
                else
                {
                    notch = new NotchView(new System.Drawing.RectangleF(0, 0, (float)Container.Bounds.Width, (float)Container.Bounds.Height), device);
                }

                notch.Bounds = notch.Frame;

                Container.Add(notch);
            }

        }

        protected override void OnDetached()
        {
        }
    }
}
