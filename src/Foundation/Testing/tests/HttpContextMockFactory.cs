namespace Sitecore.Foundation.Testing
{
  using System.IO;
  using System.Reflection;
  using System.Web;
  using System.Web.SessionState;

  public static class HttpContextMockFactory
  {
    public static HttpContext Create()
    {
      var httpRequest = new HttpRequest("", "http://google.com/", "");

      return Create(httpRequest);
    }

    public static HttpContext Create(HttpRequest httpRequest)
    {
      var stringWriter = new StringWriter();
      var httpResponse = new HttpResponse(stringWriter);

      return Create(httpRequest, httpResponse);
    }

    public static HttpContext Create(HttpRequest httpRequest, HttpResponse httpResponse)
    {
      var httpContext = new HttpContext(httpRequest, httpResponse);

      var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(), new HttpStaticObjectsCollection(), 10, true, HttpCookieMode.AutoDetect, SessionStateMode.InProc, false);

      httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, CallingConventions.Standard, new[] {typeof(HttpSessionStateContainer)}, null).Invoke(new object[] {sessionContainer});

      return httpContext;
    }
  }
}