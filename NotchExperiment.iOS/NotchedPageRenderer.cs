using System;
using CoreAnimation;
using CoreGraphics;
using NotchExperiment;
using NotchExperiment.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using static NotchExperiment.Device;
using static NotchExperiment.NotchedPage;

[assembly: ExportRenderer(typeof(NotchedPage), typeof(NotchedPageRenderer))]
namespace NotchExperiment.iOS
{
    public class NotchedPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (Element != null)
            {
                var notchedPage = Element as NotchedPage;



                Device device;
                if (notchedPage.Model == PhoneModels.Custom)
                {
                    device = notchedPage.CustomDevice;
                }
                else
                {
                    device = Devices[notchedPage.Model];
                }

                NotchView notch;
                if(DesignMode.IsDesignModeEnabled)
                {
                    notch = new NotchView(new System.Drawing.RectangleF(0, 0, (float)device.ScreenWidth, (float)device.ScreenHeight), device);//Previewer always reports size of an iphone 5
                }
                else
                {
                    notch = new NotchView(new System.Drawing.RectangleF(0, 0, (float)View.Bounds.Width, (float)View.Bounds.Height), device);
                }

                notch.Bounds = notch.Frame;

                View.Add(notch);




                //var label = new UILabel();
                //label.Text = changedCount + $"w: {notchedPage.Width} " + UIDevice.CurrentDevice.Name + View.Bounds;
                //label.Frame = new CGRect(0, 200, 500, 200);
                //label.Bounds = new CGRect(0, 0, 500, 200);
                //label.LineBreakMode = UILineBreakMode.WordWrap;


                //View.Add(label);



            }

        }

    }
}
