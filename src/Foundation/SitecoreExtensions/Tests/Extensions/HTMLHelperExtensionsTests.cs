using System;

namespace Sitecore.Foundation.SitecoreExtensions.Tests.HtmlHelper
{
    using System.Web.Mvc;
    using System.Xml.Linq;
    using FluentAssertions;
    using Sitecore.Extensions.XElementExtensions;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.Mvc;
    using Sitecore.Mvc.Common;
    using Sitecore.Mvc.Presentation;
    using Xunit;

    public class HTMLHelperExtensionsTests
  {
    [Theory]
    [AutoDbData]
    public void AddUniqueFormId_CurrentRenderingNull_ShouldReturnNull(HtmlHelper helper)
    {
      helper.AddUniqueFormId().Should().BeNull();
    }

    [Theory]
    [AutoDbData]
    public void AddUniqueFormId_CurrentRenderingNotNull_ShouldReturnHiddenInput(HtmlHelper helper)
    {
      var id = Guid.NewGuid();
      ContextService.Get().Push(new RenderingContext() {Rendering =  new Rendering() {UniqueId = id} });
      helper.Sitecore().CurrentRendering.Should().NotBeNull();
      var xml = XDocument.Parse(helper.AddUniqueFormId().ToString());
      xml.Root.Name.LocalName.Should().Be("input");
      xml.Root.GetAttributeValue("name").Should().Be("uid");
      Guid.Parse(xml.Root.GetAttributeValue("value")).Should().Be(id);
    }
  }
}
