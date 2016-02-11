namespace Sitecore.Foundation.SitecoreExtensions.Attributes
{
  using System;
  using System.Linq;
  using System.Reflection;
  using System.Web;
  using System.Web.Mvc;
  using Sitecore.Mvc.Presentation;

  public class ValidateRenderingIdAttribute : ActionMethodSelectorAttribute
  {
    public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
    {
      var ignoreCase = StringComparison.InvariantCultureIgnoreCase;

      var httpRequest = controllerContext.HttpContext.Request;
      var isWebFormsForMarketersRequest = httpRequest.Form.AllKeys
        .Any(key => key.StartsWith("wffm", ignoreCase) && key.EndsWith("FormId", ignoreCase));

      if (isWebFormsForMarketersRequest)
      {
        return false;
      }
      string renderingId;
      if (!httpRequest.GetHttpMethodOverride().Equals(HttpVerbs.Post.ToString(), ignoreCase) || string.IsNullOrEmpty(renderingId = httpRequest.Form["uid"]))
      {
        return true;
      }

      var renderingContext = RenderingContext.CurrentOrNull;
      if (renderingContext == null)
      {
        return false;
      }

      Guid id;
      return Guid.TryParse(renderingId, out id) && id.Equals(renderingContext.Rendering.UniqueId);
    }
  }
}