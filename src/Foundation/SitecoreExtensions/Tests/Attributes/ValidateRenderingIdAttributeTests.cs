namespace Sitecore.Foundation.SitecoreExtensions.Tests.Attributes
{
  using System;
  using System.Collections.Specialized;
  using System.Web.Mvc;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Foundation.SitecoreExtensions.Attributes;
  using Sitecore.Mvc.Common;
  using Sitecore.Mvc.Presentation;
  using Xunit;

  public class ValidateRenderingIdAttributeTests
  {
    [Theory]
    [AutoDbMvcData]
    public void IsValidForRequest_CurrentRenderingNull_ShouldReturnFalse(ValidateRenderingIdAttribute attribute, ControllerContext ctx, string id)
    {
      InitControllerContext(ctx);
      ctx.HttpContext.Request.Form.Add("uid", id);

      //act and assert
      attribute.IsValidForRequest(ctx, null).Should().BeFalse();
    }


    [Theory]
    [AutoDbMvcData]
    public void IsValidForRequest_WffmForm_ShouldReturnFalse(ValidateRenderingIdAttribute attribute, ControllerContext ctx, string id)
    {
      InitControllerContext(ctx);
      ctx.HttpContext.Request.Form.Add($"wffm{Guid.NewGuid()}.FormId",id);
      //act and assert
      attribute.IsValidForRequest(ctx, null).Should().BeFalse();
    }

    [Theory]
    [AutoDbMvcData]
    public void IsValidForRequest_GetRequest_ShouldReturnTrue(ValidateRenderingIdAttribute attribute, ControllerContext ctx)
    {
      InitControllerContext(ctx);
      ctx.HttpContext.Request.HttpMethod.Returns("get");
      //act and assert
      attribute.IsValidForRequest(ctx, null).Should().BeTrue();
    }

    [Theory]
    [AutoDbMvcData]
    public void IsValidForRequest_CurrentRenderingIDMatch_ShouldReturnTrue(ValidateRenderingIdAttribute attribute, ControllerContext controllerContext, Guid id)
    {
      //arrange
      InitControllerContext(controllerContext);
      controllerContext.HttpContext.Request.Form.Add("uid", id.ToString());
      ContextService.Get().Push(new RenderingContext
      {
        Rendering = new Rendering
        {
          UniqueId = id
        }
      });

      //act and assert
      attribute.IsValidForRequest(controllerContext, null).Should().BeTrue();
    }

    [Theory]
    [AutoDbMvcData]
    public void IsValidForRequest_CurrentRenderingIDNotMatch_ShouldReturnFalse(ValidateRenderingIdAttribute attribute, ControllerContext controllerContext, Guid formId, Guid id)
    {
      //arrange
      InitControllerContext(controllerContext);
      controllerContext.HttpContext.Request.Form.Add("uid", formId.ToString());
      ContextService.Get().Push(new RenderingContext
      {
        Rendering = new Rendering
        {
          UniqueId = id
        }
      });

      //act and assert
      attribute.IsValidForRequest(controllerContext, null).Should().BeFalse();
    }

    [Theory]
    [AutoDbMvcData]
    public void IsValidForRequest_RenderingIdInFormNotGuid_ShouldReturnFalse(ValidateRenderingIdAttribute attribute, ControllerContext controllerContext, string formId, Guid id)
    {
      //arrange
      InitControllerContext(controllerContext);
      controllerContext.HttpContext.Request.Form.Add("uid", formId);
      ContextService.Get().Push(new RenderingContext
      {
        Rendering = new Rendering
        {
          UniqueId = id
        }
      });

      //act and assert
      attribute.IsValidForRequest(controllerContext, null).Should().BeFalse();
    }

    [Theory]
    [AutoDbMvcData]
    public void IsValidForRequest_FormWithoutRenderingId_ShouldReturnTrue(ValidateRenderingIdAttribute attribute, ControllerContext controllerContext, string formId, Guid id)
    {
      //arrange
      InitControllerContext(controllerContext);
      //act and assert
      attribute.IsValidForRequest(controllerContext, null).Should().BeTrue();
    }

    private static void InitControllerContext(ControllerContext ctx)
    {
      ctx.HttpContext.Request.HttpMethod.Returns("post");
      ctx.HttpContext.Request.Form.Returns(new NameValueCollection());
      ctx.HttpContext.Request.Headers.Returns(new NameValueCollection());
      ctx.HttpContext.Request.QueryString.Returns(new NameValueCollection());
    }
  }
}