namespace Sitecore.Feature.Demo.Models.Repository
{
  using Sitecore.Analytics;
  using Sitecore.CES.DeviceDetection;

  internal class DeviceRepository
  {
    private Device _current;

    public Device GetCurrent()
    {
      if (_current != null)
      {
        return _current;
      }

      if (!DeviceDetectionManager.IsEnabled || !DeviceDetectionManager.IsReady || string.IsNullOrEmpty(Tracker.Current.Interaction.UserAgent))
      {
        return null;
      }

      return _current = CreateDevice(DeviceDetectionManager.GetDeviceInformation(Tracker.Current.Interaction.UserAgent));
    }

    private Device CreateDevice(DeviceInformation deviceInformation)
    {
      return new Device()
             {
               Title = string.Join(", ", deviceInformation.DeviceVendor, deviceInformation.DeviceModelName),
               Browser = deviceInformation.Browser
              };
    }
  }
}