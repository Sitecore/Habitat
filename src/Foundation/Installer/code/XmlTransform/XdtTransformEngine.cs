namespace SitecoreFoundation.Installer.XmlTransform
{
  using Microsoft.Web.XmlTransform;

  public class XdtTransformEngine: IXdtTransformEngine
  {
    public void ApplyConfigTransformation(string xmlFile, string transformFile, string targetFile)
    {
      var source = new XmlTransformableDocument
      {
        PreserveWhitespace = true
      };
      source.Load(xmlFile);


      var transform = new XmlTransformation(transformFile);
      if (transform.Apply(source))
      {
        source.Save(xmlFile);
      }
    }
  }
}