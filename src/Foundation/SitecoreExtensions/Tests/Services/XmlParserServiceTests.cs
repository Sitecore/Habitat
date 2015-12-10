namespace Sitecore.Foundation.SitecoreExtensions.Tests.Services
{
  using System;
  using System.Xml;
  using FluentAssertions;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Foundation.SitecoreExtensions.Tests.Common;
  using Xunit;

  public class XmlParserServiceTests
  {
    public class TestXmlParser : XmlParserService
    {
      public XmlDocument ValidateAndReturnXmlDocument(string xml)
      {
        return XmlParserService.ValidateAndReturnXmlDocument(xml);
      }

      public string GetAttribute(XmlNode node, string attributeName)
      {
        return XmlParserService.GetAttribute(node, attributeName);
      }
    }

    [Theory]
    [AutoDbData]
    public void ValidateAndReturnXmlDocument_EmptyString_ShouldReturnNull(TestXmlParser service)
    {
      service.ValidateAndReturnXmlDocument(string.Empty).Should().BeNull();
    }

    [Theory]
    [AutoDbData]
    public void ValidateAndReturnXmlDocument_NotExpectedXml_ShouldThrowException(TestXmlParser service)
    {
      Action a = () => service.ValidateAndReturnXmlDocument("<data/>");
      a.ShouldThrow<ArgumentException>().WithMessage("Xml is not a valid image");
    }

    [Theory]
    [AutoDbData]
    public void ValidateAndReturnXmlDocument_ExpectedXml_ShouldReturnXmlDocument(TestXmlParser service)
    {
      var validateAndReturnXmlDocument = service.ValidateAndReturnXmlDocument("<image/>");
      validateAndReturnXmlDocument.Should().NotBeNull();
      validateAndReturnXmlDocument.DocumentElement.Name.Should().Be("image");
    }


    [Theory]
    [AutoDbData]
    public void GetAttributeShouldReturn_NotExistentAttribute_EmptyString(TestXmlParser service)
    {

      var doc = new XmlDocument();
      doc.LoadXml("<data/>");
      
      var attribute = service.GetAttribute(doc.DocumentElement,"someRandomAttribute");
      attribute.Should().BeNullOrEmpty();
    }


    [Theory]
    [AutoDbData]
    public void GetAttributeShould_ValidAttribute_ReturnValue(TestXmlParser service)
    {

      var doc = new XmlDocument();
      doc.LoadXml("<data expectedAttr='expectedValue'/>");

      var attribute = service.GetAttribute(doc.DocumentElement, "expectedAttr");
      attribute.Should().Be("expectedValue");
    }
  }
}