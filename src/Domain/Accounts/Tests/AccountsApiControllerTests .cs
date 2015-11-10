namespace Habitat.Accounts.Tests
{
  using FluentAssertions;
  using Habitat.Accounts.Controllers;
  using Habitat.Accounts.Models;
  using Repositories;
  using Habitat.Accounts.Services;
  using Habitat.Accounts.Tests.Extensions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class AccountsApiControllerTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldReturnAuthenticatedResultIfLoggedIn([Frozen] IAccountRepository repo, LoginCredentials credentials, IAccountsSettingsService service)
    {
      repo.Login(string.Empty, string.Empty).ReturnsForAnyArgs(true);
      var controller = new AccountsApiController(repo);
      var result = controller.Login(credentials);
      result.IsAuthenticated.Should().BeTrue();
    }

    [Theory]
    [AutoDbData]
    public void ShouldReturnNotAuthenticatedResultIfNotLoggedIn([Frozen] IAccountRepository repo, LoginCredentials credentials, IAccountsSettingsService service)
    {
      repo.Login(string.Empty, string.Empty).ReturnsForAnyArgs(false);
      var controller = new AccountsApiController(repo);
      var result = controller.Login(credentials);
      result.IsAuthenticated.Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void SholdReturnValidationErrorIfModelNotValid([NoAutoProperties]AccountsApiController controller, LoginCredentials credentials, IAccountsSettingsService service)
    {
      var errorMessage = "Error";
      controller.ModelState.AddModelError("Error", errorMessage);
      var result = controller.Login(credentials);
      result.ValidationMessage.ShouldBeEquivalentTo(errorMessage);
    }
  }
}