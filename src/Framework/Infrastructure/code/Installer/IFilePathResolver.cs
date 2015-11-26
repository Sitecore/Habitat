namespace Habitat.Framework.Infrastructure.Installer
{
  public interface IFilePathResolver
  {
    string MapPath(string relativePath);
  }
}