using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NotchExperiment
{
    public class Device
    {
        public enum PhoneModels
        {
            Default,
            iPhoneX,
            iPhoneXS,
            iPhoneXSMax,
            iPhoneXR,
            OnePlus6,
            P30,
            LGG7,
            Pixel3XL,
            Custom

        }

        //common
        public float CornerRadius { get; set; } = 40;
        public float NotchTopRadius { get; set; } = 4;
        public float NotchBottomRadius { get; set; } = 20;
        public float NotchX { get; set; } = 50;
        public float NotchY { get; set; } = 0;

        public float NotchWidth { get; set; } = 100;
        public float NotchHeight { get; set; } = 30;
        public Color NotchColor { get; set; } = Color.FromHex("#2d2d2d");

        //iOS Only
        public float ScreenWidth { get; set; } = 414;
        public float ScreenHeight { get; set; } = 736;


        //Android only
        public float StatusBarHeight { get; set; }
        public float StatusBarColor { get; set; }
        public float BottomOffset { get; set; }




        public static Dictionary<PhoneModels, Device> Devices { get; } = new Dictionary<PhoneModels, Device>
        {
            { PhoneModels.Default, new Device{NotchX = 0, CornerRadius = 0, NotchWidth = 0, NotchBottomRadius = 0 } },
            { PhoneModels.iPhoneX, new Device{NotchX = 84, CornerRadius = 39, NotchWidth = 206, NotchBottomRadius = 20, ScreenWidth = 375, ScreenHeight = 812 } },
            { PhoneModels.iPhoneXS, new Device{NotchX = 84, CornerRadius = 39, NotchWidth = 206, NotchBottomRadius = 20, ScreenWidth = 375, ScreenHeight = 812 } },
            { PhoneModels.iPhoneXR, new Device{NotchX = 92, CornerRadius = 40, NotchWidth = 230, NotchBottomRadius = 20, ScreenWidth = 414, ScreenHeight = 896 } },
            { PhoneModels.iPhoneXSMax, new Device{NotchX = 103, CornerRadius = 39, NotchWidth = 209, NotchBottomRadius = 20, ScreenWidth = 414, ScreenHeight = 896 } },
            { PhoneModels.Pixel3XL, new Device{NotchX = 260, CornerRadius = 45, NotchWidth = 300, NotchBottomRadius = 45,NotchHeight = 80, } },
            { PhoneModels.P30, new Device{NotchX = 370, CornerRadius = 45, NotchWidth = 80, NotchTopRadius = 18, NotchBottomRadius = 40, NotchHeight = 58 } },
        };
    }
}
