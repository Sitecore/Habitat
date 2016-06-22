namespace Sitecore.Feature.Accounts.Tests.Attributes
{
  using System.Web.Mvc;
  using FluentAssertions;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Feature.Accounts.Attributes;
  using Sitecore.Feature.Accounts.Tests.Extensions;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class ValidateModelTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldNotChangeResultIfValidModel([Frozen]ControllerBase controller, [Substitute]ActionExecutingContext filterContext, ValidateModelAttribute validateModel)
    {
      filterContext.Controller = controller;

      validateModel.OnActionExecuting(filterContext);

      filterContext.Result.Should().BeNull();
    }

    [Theory]
    [AutoDbData]
    public void ShouldChangeResultIfInvalidModel([Frozen]ControllerBase controller, [Substitute]ActionExecutingContext filterContext, ValidateModelAttribute validateModel)
    {
      filterContext.Controller = controller;
      filterContext.Controller.ViewData.ModelState.AddModelError("error","error");

      validateModel.OnActionExecuting(filterContext);

      filterContext.Result.Should().BeOfType<ViewResult>().Which.ViewData.ShouldBeEquivalentTo(filterContext.Controller.ViewData);
    }
  }
}
