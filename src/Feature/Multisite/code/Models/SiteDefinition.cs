namespace Sitecore.Feature.MultiSite.Models
{
  using Sitecore.Text;

  public class SiteDefinition
  {
    public string Name { get; set; }
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
  }
}