using System;
using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace NotchExperiment.iOS
{
    public class NotchView : UIView
    {
        public NotchView(RectangleF frame, Device device) : base(frame)
        {
            Device = device;
            BackgroundColor = UIColor.Clear;
            Opaque = false;
        }

        public Device Device { get; private set; }

        public static nfloat Right = 0;
        public static nfloat Bottom = (System.nfloat)(Math.PI / 2);
        public static nfloat Left = (System.nfloat)Math.PI;
        public static nfloat Top = (System.nfloat)(Math.PI * 1.5);

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            Console.WriteLine("DrawingView draw!");
            CGContext context = UIGraphics.GetCurrentContext();

            // Create a path around the entire view
            UIBezierPath clipPath = UIBezierPath.FromRect(rect);

            // Add the transparent window to a sample rectangle
            CGRect sampleRect = new CGRect(0f, 0f, rect.Width , rect.Height);
            UIBezierPath path = UIBezierPath.FromRoundedRect(sampleRect, (System.nfloat)Device.CornerRadius);

            //Top left of notch
            //path.MoveTo(new CGPoint(96, 0));
            path.MoveTo(new CGPoint(Device.NotchX - Device.NotchTopRadius, Device.NotchY));


            //Curve into notch
            //path.AddArc(new CGPoint(96, 4), 4, (System.nfloat)(Math.PI * 1.5), (System.nfloat)0, true);//Angles in Radians, relative to 90deg
            path.AddArc(new CGPoint(Device.NotchX - Device.NotchTopRadius, Device.NotchTopRadius), (System.nfloat)(Device.NotchY + Device.NotchTopRadius), Top, Right, true);//Angles in Radians, relative to 90deg

            //Left side of notch
            //path.AddLineTo(new CGPoint(Device.NotchX + Device.NotchTopRadius, 42));
            path.AddLineTo(new CGPoint(Device.NotchX , Device.NotchHeight - Device.NotchBottomRadius));

            //Curve into bottom
            //path.AddArc(new CGPoint(108, 42), 8, (System.nfloat)Math.PI, (System.nfloat)Math.PI/2, false);
            path.AddArc(new CGPoint(Device.NotchX + Device.NotchBottomRadius ,Device.NotchY + Device.NotchHeight - Device.NotchBottomRadius), (System.nfloat)Device.NotchBottomRadius, Left, Bottom, false);

            //Bottom of notch
            //path.AddLineTo(new CGPoint(200, 50));
            path.AddLineTo(new CGPoint(Device.NotchX + Device.NotchWidth - Device.NotchBottomRadius, Device.NotchY + Device.NotchHeight));





            //Curve into right
            //path.AddArc(new CGPoint(200, 42), 8, (System.nfloat)(Math.PI / 2), 0, false);
            path.AddArc(new CGPoint(Device.NotchX + Device.NotchWidth - Device.NotchBottomRadius, Device.NotchY + Device.NotchHeight - Device.NotchBottomRadius), (System.nfloat)Device.NotchBottomRadius, Bottom, Right, false);

            //Right side of nothc
            //path.AddLineTo(new CGPoint(208, 4));
            path.AddLineTo(new CGPoint(Device.NotchX + Device.NotchWidth, Device.NotchY + Device.NotchTopRadius));


            //Curve out of notch
            path.AddArc(new CGPoint(Device.NotchX + Device.NotchWidth + Device.NotchTopRadius, Device.NotchTopRadius), (System.nfloat)(Device.NotchY + Device.NotchTopRadius), Left, Top, true);

            //finish path
            path.AddLineTo(new CGPoint(Device.NotchX - Device.NotchTopRadius, Device.NotchY));
            path.ClosePath();
            clipPath.AppendPath(path);

            // This sets the algorithm used to determine what gets filled and what doesn't
            clipPath.UsesEvenOddFillRule = true;


            context.SetFillColor(Device.NotchColor.ToUIColor().CGColor);
            //context.SetAlpha(0.6f);

            clipPath.Fill();


        }
    }
}

