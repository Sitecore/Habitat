namespace Sitecore.Foundation.Testing.Builders
{
  using System.Web;
  using System.Web.Mvc;
  using System.Web.Routing;
  using NSubstitute;
  using Ploeh.AutoFixture.Kernel;

  public class HtmlHelperBuilder : ISpecimenBuilder
  {
    public object Create(object request, ISpecimenContext context)
    {
      if (typeof(HtmlHelper).Equals(request))
      {
        return this.GetHtmlHelper();
      }

      return new NoSpecimen();
    }

    private HtmlHelper GetHtmlHelper(string routeController = "", string routeAction = "")
    {
      return new HtmlHelper(this.GetViewContext(routeController, routeAction), new ViewPage());
    }

    private ViewContext GetViewContext(string routeController = "", string routeAction = "")
    {
      var routeData = new RouteData();
      routeData.Values["controller"] = routeController;
      routeData.Values["action"] = routeAction;

      var httpContext = Substitute.For<HttpContextBase>();
      var viewContext = Substitute.For<ViewContext>();
      var httpRequest = Substitute.For<HttpRequestBase>();
      var httpResponse = Substitute.For<HttpResponseBase>();

      httpContext.Request.Returns(httpRequest);
      httpContext.Response.Returns(httpResponse);
      httpResponse.ApplyAppPathModifier(Arg.Any<string>()).Returns(string.Format("/{0}/{1}", routeController, routeAction));

      viewContext.HttpContext = httpContext;
      viewContext.RequestContext = new RequestContext(httpContext, routeData);
      viewContext.RouteData = routeData;
      viewContext.ViewData = Substitute.For<ViewDataDictionary>();
      viewContext.ViewData.Model = null;
      viewContext.TempData = Substitute.For<TempDataDictionary>();
      return viewContext;
    }
  }
}