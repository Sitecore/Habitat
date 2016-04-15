namespace Sitecore.Foundation.Theming.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Foundation.Theming.Extensions;
  using Sitecore.Foundation.Theming.Tests.Extensions;
  using Sitecore.Mvc.Presentation;
  using Xunit;
  using Ploeh.AutoFixture.AutoNSubstitute;
  public class RenderingExtensionsTests
  {
    [Theory]
    [AutoDbData]
    public void IsFixedHeight_PropertySetInRendering_ShouldReturnTrue([Substitute]Rendering rendering)
    {
      var parameters = new RenderingParameters("Fixed height=1");
      rendering.Parameters.Returns(parameters);
      rendering.IsFixedHeight().Should().BeTrue();
    }

    [Theory]
    [AutoDbData]
    public void IsFixedHeight_PropertyIsNotSet_ShouldReturnTrue([Substitute]Rendering rendering)
    {
      var parameters = new RenderingParameters("");
      rendering.Parameters.Returns(parameters);
      rendering.IsFixedHeight().Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void IsFixedHeight_PropertySetToZero_ShouldReturnTrue([Substitute]Rendering rendering)
    {
      var parameters = new RenderingParameters("Fixed height=0");
      rendering.Parameters.Returns(parameters);
      rendering.IsFixedHeight().Should().BeFalse();
    }
  }
}
