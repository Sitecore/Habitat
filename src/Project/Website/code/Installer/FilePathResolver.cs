namespace Habitat.Website.Installer
{
  using System.Web;

  public class FilePathResolver:IFilePathResolver
  {
    public string MapPath(string relativePath)
    {
      return HttpContext.Current.Server.MapPath(relativePath);
    }
  }
}