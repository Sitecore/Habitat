using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SitecoreExtensions.Attributes
{
  using System.Reflection;
  using System.Web.Mvc;
  using Sitecore.Mvc.Presentation;

  public class ValidateRenderingIdAttribute : ActionMethodSelectorAttribute
  {
    
    public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
    {
      if (!controllerContext.HttpContext.Request.GetHttpMethodOverride()
          .Equals(HttpVerbs.Post.ToString(), StringComparison.OrdinalIgnoreCase)||
          string.IsNullOrEmpty(controllerContext.HttpContext.Request.Form["uid"])) return true;
      var renderingContext = RenderingContext.CurrentOrNull;
      if (renderingContext == null)
      {
        return false;
      }

      var renderingId = controllerContext.HttpContext.Request.Form["uid"];
      Guid id;
      return Guid.TryParse(renderingId, out id) && id.Equals(renderingContext.Rendering.UniqueId);
    }
  }
}