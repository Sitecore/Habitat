namespace Sitecore.Framework.Installer.XmlTransform
{
  public interface IFilePathResolver
  {
    string MapPath(string relativePath);
  }
}