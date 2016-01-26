namespace Sitecore.Foundation.Installer.Tests
{
  using System.IO;
  using System.Linq;
  using System.Xml.Linq;
  using FluentAssertions;
  using Sitecore.Foundation.Installer.XmlTransform;
  using Xunit;

  public class XdtTransformEngineTests
  {
    public string Transform => "<configuration xmlns:xdt='http://schemas.microsoft.com/XML-Document-Transform'><node xdt:Transform='Insert'/></configuration>";

    [Fact]
    public void ApplyConfigTransformationShouldNotWriteFileIfError()
    {
      var postStep = new XdtTransformEngine();
      var configFile = Path.GetRandomFileName();
      var transformFile = Path.GetRandomFileName();

      var config = XDocument.Parse("<configuration/>");
      config.Save(configFile);


      var transform = XDocument.Parse(this.Transform);
      transform.Save(transformFile);


      //act
      postStep.ApplyConfigTransformation(configFile, transformFile, configFile);

      //assert
      var doc = XDocument.Load(configFile);
      doc.Root.HasElements.Should().Be(true, "XML root should have one child after transform");
      doc.Root.Descendants().Single().Name.LocalName.Should().Be("node");

      //teardown
      File.Delete(configFile);
      File.Delete(transformFile);
    }

  }
}