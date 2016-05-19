namespace Sitecore.Feature.Demo.Repositories
{
  using Sitecore.Analytics;
  using Sitecore.CES.DeviceDetection;
  using Sitecore.Feature.Demo.Models;

  internal class DeviceRepository
  {
    private Device _current;

    public Device GetCurrent()
    {
      if (this._current != null)
      {
        return this._current;
      }

      if (!DeviceDetectionManager.IsEnabled || !DeviceDetectionManager.IsReady || string.IsNullOrEmpty(Tracker.Current.Interaction.UserAgent))
      {
        return null;
      }

      return this._current = this.CreateDevice(DeviceDetectionManager.GetDeviceInformation(Tracker.Current.Interaction.UserAgent));
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