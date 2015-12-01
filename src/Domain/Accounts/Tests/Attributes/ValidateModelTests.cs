namespace Habitat.Accounts.Tests.Attributes
{
  using System.Web.Mvc;
  using FluentAssertions;
  using Habitat.Accounts.Attributes;
  using Habitat.Accounts.Tests.Extensions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
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
