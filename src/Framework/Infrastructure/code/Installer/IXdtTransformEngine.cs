namespace Habitat.Framework.Infrastructure.Installer
{
  public interface IXdtTransformEngine
  {
    void ApplyConfigTransformation(string xmlFile, string transformFile, string targetFile);

  }
}