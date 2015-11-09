namespace Habitat.Demo.Models
{
  public class PageLink
  {
    public PageLink(string title, string url, bool openInNewWindow)
    {
      Title = title;
      Url = url;
      OpenInNewWindow = openInNewWindow;      
    }

    public string Title
    {
      get;
      set;
    }

    public string Url
    {
      get;
      set;
    }

    public bool OpenInNewWindow
    {
      get;
      set;
    }  
  }
}