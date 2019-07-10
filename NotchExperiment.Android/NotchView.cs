
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;

namespace NotchExperiment.Droid
{
    public class NotchView : View
    {
        public Device Device { get; internal set; }

        public NotchView(Context context) :
            base(context)
        {
            Initialize();
        }

        public NotchView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public NotchView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        void Initialize()
        {

        }


        float right = 0;
        float bottom = 90;
        float left = 180;
        float top = 270;
        float clockwiseQuarter = 90;
        float antiClockwiseQuarter = -90;

        float topYOffset => 0 - Device.StatusBarHeight;
        float bottomYOffset => Xamarin.Forms.DesignMode.IsDesignModeEnabled ? topYOffset: 0 ; 

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);


            var clipPath = new Path();

            //Top left
            clipPath.MoveTo((float)Device.CornerRadius, topYOffset);
            clipPath.LineTo(0, topYOffset);
            clipPath.LineTo(0, (float)Device.CornerRadius + topYOffset);
            clipPath.ArcTo(0, 0 + topYOffset, (float)(Device.CornerRadius * 2), topYOffset + (float)(Device.CornerRadius * 2), left, clockwiseQuarter, true);
            clipPath.Close();

            //Notch
            var topLeftX = Device.NotchX - Device.NotchTopRadius;
            var topLeftY = topYOffset + Device.NotchY;
            clipPath.MoveTo(topLeftX, topLeftY);

            float point1x = Device.NotchX;
            float point1y = topYOffset + Device.NotchY + Device.NotchTopRadius;
            clipPath.LineTo(point1x, point1y);

            var point2x = Device.NotchX;
            var point2y = topYOffset + Device.NotchY + Device.NotchHeight - Device.NotchBottomRadius;
            clipPath.LineTo(point2x, point2y);

            var point3x = Device.NotchX + Device.NotchBottomRadius;
            var point3y = topYOffset + Device.NotchY + Device.NotchHeight;
            clipPath.LineTo(point3x, point3y);

            var point4x = Device.NotchX + Device.NotchWidth - Device.NotchBottomRadius;
            var point4y = topYOffset + Device.NotchY + Device.NotchHeight;
            clipPath.LineTo(point4x, point4y);

            var point5x = Device.NotchX + Device.NotchWidth;
            var point5y = topYOffset + Device.NotchY + Device.NotchHeight - Device.NotchBottomRadius;
            clipPath.LineTo(point5x, point5y);

            var point6x = Device.NotchX + Device.NotchWidth;
            var point6y = topYOffset + Device.NotchY + Device.NotchTopRadius;
            clipPath.LineTo(point6x, point6y);

            var point7x = Device.NotchX + Device.NotchWidth + Device.NotchTopRadius;
            var point7y = topYOffset + Device.NotchY;
            clipPath.LineTo(point7x, point7y);
            clipPath.LineTo(topLeftX, topLeftY);
            clipPath.Close();


            var arcInOval = new RectF(((float)(Device.NotchX - Device.NotchTopRadius * 2)), topYOffset + Device.NotchY, Device.NotchX, topYOffset + Device.NotchY + Device.NotchTopRadius * 2);
            clipPath.AddArc(arcInOval, top, clockwiseQuarter);

            var arcOval1 = new RectF(Device.NotchX, topYOffset + Device.NotchY + Device.NotchHeight - 2 * Device.NotchBottomRadius, Device.NotchX + 2 * Device.NotchBottomRadius, topYOffset + Device.NotchY + Device.NotchHeight);
            clipPath.AddArc(arcOval1, left, antiClockwiseQuarter);

            var arcOval2 = new RectF(Device.NotchX + Device.NotchWidth - 2 * Device.NotchBottomRadius, topYOffset + Device.NotchY + Device.NotchHeight - 2 * Device.NotchBottomRadius, Device.NotchX + Device.NotchWidth, topYOffset + Device.NotchY + Device.NotchHeight);
            clipPath.AddArc(arcOval2, bottom, antiClockwiseQuarter);

            var arcOval3 = new RectF(Device.NotchX + Device.NotchWidth, topYOffset + Device.NotchY, Device.NotchX + Device.NotchWidth + 2 * Device.NotchTopRadius, topYOffset + Device.NotchY + 2 * Device.NotchTopRadius);
            clipPath.AddArc(arcOval3, left, clockwiseQuarter);

            //Top Right
            clipPath.MoveTo(canvas.Width, topYOffset + (float)Device.CornerRadius);
            clipPath.LineTo(canvas.Width, topYOffset);
            clipPath.LineTo(canvas.Width - Device.CornerRadius, topYOffset +  0);
            clipPath.ArcTo(canvas.Width - Device.CornerRadius * 2, topYOffset , canvas.Width, topYOffset + (float)Device.CornerRadius * 2, top, clockwiseQuarter, true);
            clipPath.Close();


            //Bottom Right
            clipPath.MoveTo(canvas.Width, bottomYOffset + canvas.Height - Device.CornerRadius);
            clipPath.ArcTo((canvas.Width - Device.CornerRadius * 2), topYOffset + canvas.Height - Device.CornerRadius * 2, canvas.Width, bottomYOffset + canvas.Height, right, clockwiseQuarter, true);
            clipPath.LineTo(canvas.Width, bottomYOffset + canvas.Height);
            clipPath.LineTo(canvas.Width, bottomYOffset + canvas.Height - Device.CornerRadius);
            clipPath.Close();

            //Bottom Left
            clipPath.MoveTo(Device.CornerRadius, topYOffset + canvas.Height);
            clipPath.ArcTo(0, bottomYOffset + canvas.Height - Device.CornerRadius * 2, Device.CornerRadius * 2, bottomYOffset + canvas.Height, bottom, clockwiseQuarter, true);
            clipPath.LineTo(0, canvas.Height);
            clipPath.LineTo(Device.CornerRadius, bottomYOffset + canvas.Height);

            canvas.ClipPath(clipPath);

            var paint = new Paint()
            {
                Color = Device.NotchColor.ToAndroid(),
                AntiAlias = true,
            };

            canvas.DrawRect(new Rect(0, 0, canvas.Width, canvas.Height), paint);

        }
    }

}
