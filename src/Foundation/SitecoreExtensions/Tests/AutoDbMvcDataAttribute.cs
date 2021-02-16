namespace Sitecore.Foundation.SitecoreExtensions.Tests
{
    using System.Web.Mvc;
    using Sitecore.Foundation.Testing.Attributes;

    public class AutoDbMvcDataAttribute : AutoDbDataAttribute
  {
    public AutoDbMvcDataAttribute()
    {
      Fixture.Customize<ControllerContext>(c => c.Without(x => x.DisplayMode));
    }
  }
}