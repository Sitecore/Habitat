namespace Habitat.Website.Installer
{
  public interface IFilePathResolver
  {
    string MapPath(string relativePath);
  }
}