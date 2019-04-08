namespace Sitecore.Feature.Demo.Repositories
{
    using Sitecore.Analytics;
    using Sitecore.CES.DeviceDetection;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Foundation.DependencyInjection;

    [Service]
    public class DeviceRepository
    {
        private Device current;

        public Device GetCurrent()
        {
            if (this.current != null)
            {
                return this.current;
            }

            if (!DeviceDetectionManager.IsEnabled || !DeviceDetectionManager.IsReady || string.IsNullOrEmpty(Tracker.Current.Interaction.UserAgent))
            {
                return null;
            }

            return this.current = this.CreateDevice(DeviceDetectionManager.GetDeviceInformation(Tracker.Current.Interaction.UserAgent));
        }

        private Device CreateDevice(DeviceInformation deviceInformation)
        {
            return new Device
            {
                Title = string.Join(", ", deviceInformation.DeviceVendor, deviceInformation.DeviceModelName),
                Browser = deviceInformation.Browser
            };
        }
    }
}