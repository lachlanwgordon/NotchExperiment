using System;
using System.ComponentModel;
using Xamarin.Forms;
using static NotchExperiment.Device;

namespace NotchExperiment
{
    [DesignTimeVisible(true)]
    public class NotchEffect : RoutingEffect
    {
        public NotchEffect() : base($"NotchExperiment.{nameof(NotchEffect)}")
        {
        }

        public PhoneModels Model { get; set; }
        public Device CustomDevice { get; set; } = new Device();
    }
}
