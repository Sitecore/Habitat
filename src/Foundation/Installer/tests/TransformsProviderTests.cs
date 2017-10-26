namespace Sitecore.Foundation.Installer.Tests
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Foundation.Installer.XmlTransform;
  using Xunit;

  public class TransformsProviderTests
  {
    [Theory]
    [AutoSububstituteData]
    public void GetTransformsByLayer_ShouldReturnsTransformsList([Frozen]IFilePathResolver path, [Greedy]TransformProvider provider, string layerName, string transformFileName)
    {
      var constructorTest = new TransformProvider();
      var transformsPath = $"{Directory.GetCurrentDirectory()}\\temp\\transforms";
      path.MapPath(Arg.Any<string>()).Returns(transformsPath);
      Directory.CreateDirectory("temp");
      Directory.CreateDirectory(transformsPath);
      Directory.CreateDirectory($"{transformsPath}/{layerName}");
      var transformFilePath = $"{transformsPath}\\{layerName}\\{transformFileName}.transform";
      File.CreateText(transformFilePath);
      var transforms = provider.GetTransformsByLayer(layerName);
      transforms.Should().Contain(new List<string>() { transformFilePath });
    }

    [Theory]
    [AutoSububstituteData]
    public void GetTransformsByLayer_LayerFolderDoesNotExists_ShouldReturnsEmptyCollection([Frozen] IFilePathResolver path, [Greedy] TransformProvider provider, string layerName, string transformFileName)
    {
      var transformsPath = $"{Directory.GetCurrentDirectory()}\\temp\\transforms";
      path.MapPath(Arg.Any<string>()).Returns(transformsPath);
      Directory.CreateDirectory("temp");
      Directory.CreateDirectory(transformsPath);
      var transforms = provider.GetTransformsByLayer(layerName);
      transforms.Should().BeEmpty();
    }

    [Theory]
    [AutoSububstituteData]
    public void GetTransformsByLayer_TransformsFolderDoesNotExists_ShouldReturnsEmptyCollection([Frozen] IFilePathResolver path, [Greedy] TransformProvider provider, string layerName, string transformFileName)
    {
      path.MapPath(Arg.Any<string>()).Returns(string.Empty);
      Directory.CreateDirectory("temp");
      var transforms = provider.GetTransformsByLayer(layerName);
      transforms.Should().BeEmpty();
    }
  }
}
