namespace Sitecore.Foundation.SitecoreExtensions.Tests.Attributes
{
  using System;
  using System.Collections.Specialized;
  using System.Web.Mvc;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Foundation.SitecoreExtensions.Attributes;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Mvc.Common;
  using Sitecore.Mvc.Presentation;
  using Xunit;

  public class ValidateRenderingIdAttributeTests
  {
    [Theory]
    [AutoDbData]
    public void IsValidForRequest_CurrentRenderingNull_ShouldReturnFalse(ValidateRenderingIdAttribute attribute)
    {
      //act and assert
      attribute.IsValidForRequest(null, null).Should().BeFalse();
    }

    [Theory]
    [AutoDbMvcData]
    public void IsValidForRequest_CurrentRenderingIDMatch_ShouldReturnTrue(ValidateRenderingIdAttribute attribute, ControllerContext controllerContext, Guid id)
    {
      //arrange
      controllerContext.HttpContext.Request.Form.Returns(new NameValueCollection() { { "uid", id.ToString() } });
      ContextService.Get().Push(new RenderingContext() { Rendering = new Rendering() { UniqueId = id } });

      //act and assert
      attribute.IsValidForRequest(controllerContext, null).Should().BeTrue();
    }

    [Theory]
    [AutoDbMvcData]
    public void IsValidForRequest_CurrentRenderingIDNotMatch_ShouldReturnFalse(ValidateRenderingIdAttribute attribute, ControllerContext controllerContext, Guid formId,Guid id)
    {
      //arrange
      controllerContext.HttpContext.Request.Form.Returns(new NameValueCollection() { { "uid", formId.ToString() } });
      ContextService.Get().Push(new RenderingContext() { Rendering = new Rendering() { UniqueId = id } });

      //act and assert
      attribute.IsValidForRequest(controllerContext, null).Should().BeFalse();
    }

    [Theory]
    [AutoDbMvcData]
    public void IsValidForRequest_RenderingIdInFormNotGuid_ShouldReturnFalse(ValidateRenderingIdAttribute attribute, ControllerContext controllerContext, string formId, Guid id)
    {
      //arrange
      controllerContext.HttpContext.Request.Form.Returns(new NameValueCollection() { { "uid", formId } });
      ContextService.Get().Push(new RenderingContext() { Rendering = new Rendering() { UniqueId = id } });

      //act and assert
      attribute.IsValidForRequest(controllerContext, null).Should().BeFalse();
    }

    [Theory]
    [AutoDbMvcData]
    public void IsValidForRequest_FormWithoutRenderingId_ShouldReturnFalse(ValidateRenderingIdAttribute attribute, ControllerContext controllerContext, string formId, Guid id)
    {
      //arrange
      controllerContext.HttpContext.Request.Form.Returns(new NameValueCollection() {});
      ContextService.Get().Push(new RenderingContext() { Rendering = new Rendering() { UniqueId = id } });

      //act and assert
      attribute.IsValidForRequest(controllerContext, null).Should().BeFalse();
    }
  }
}