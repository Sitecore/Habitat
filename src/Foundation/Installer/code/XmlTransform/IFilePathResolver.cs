namespace SitecoreFoundation.Installer.XmlTransform
{
  public interface IFilePathResolver
  {
    string MapPath(string relativePath);
  }
}