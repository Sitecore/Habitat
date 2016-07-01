namespace Sitecore.Foundation.Installer.XmlTransform
{
  public interface IXdtTransformEngine
  {
    void ApplyConfigTransformation(string xmlFile, string transformFile, string targetFile);
  }
}