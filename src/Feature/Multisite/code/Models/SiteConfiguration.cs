namespace Sitecore.Feature.Multisite.Models
{
  using Sitecore.Text;

  public class SiteConfiguration
  {
    public string Name { get; set; }
    public string Title { get; set; }
    public string HostName { get; set; }
    public bool IsCurrent { get; set; }

    public string Url
    {
      get
      {
        var url = new UrlString
                  {
                    HostName = this.HostName
                  };

        return url.ToString();
      }
    }

    public bool ShowInMenu { get; set; }
  }
}