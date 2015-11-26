namespace Habitat.Framework.Infrastructure.Installer
{
  using System.Web.Hosting;

  public class FilePathResolver:IFilePathResolver
  {
    public string MapPath(string relativePath)
    {
      return HostingEnvironment.MapPath(relativePath);
    }
  }
}