namespace Sitecore.Foundation.Installer.XmlTransform
{
  public interface IFilePathResolver
  {
    string MapPath(string relativePath);
  }
}