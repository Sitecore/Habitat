namespace Sitecore.Feature.Demo.Models
{
  public class PageLink
  {
    public PageLink(string title, string url, bool openInNewWindow)
    {
      this.Title = title;
      this.Url = url;
      this.OpenInNewWindow = openInNewWindow;
    }

    public string Title { get; set; }

    public string Url { get; set; }

    public bool OpenInNewWindow { get; set; }
  }
}