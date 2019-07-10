using System;
using System.ComponentModel;
using Xamarin.Forms;
using static NotchExperiment.Device;

namespace NotchExperiment
{
    [DesignTimeVisible(true)]
    public class NotchedPage : ContentPage
    {
        public PhoneModels Model { get; set; }

        public Device CustomDevice { get; set; } = new Device();
        

        public NotchedPage()
        {
        }

    }
}
