namespace Sitecore.Foundation.Installer.Tests
{
  using System;
  using System.Web.Security;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.FakeDb.Security.Web;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class AccountsEnableActionTests
  {
    [Theory]
    [AutoDbData]
    public void Run_SetOfUsers_ShouldEnableUsers([Frozen]MembershipProvider provider, MembershipSwitcher switcher, AccountsEnableAction accountsEnableAction)
    {
      //Arrange
      int total;
      var disabledUser = this.GetUser("john", false);
      var enabledUser = this.GetUser("smith", true);
      provider.GetAllUsers(0, 0, out total).ReturnsForAnyArgs(x=>new MembershipUserCollection() {enabledUser, disabledUser});
      provider.GetUser(Arg.Any<string>(), Arg.Any<bool>()).Returns(x => new MembershipUserCollection() { enabledUser, disabledUser }[x.Arg<string>()]);

      //Act
      accountsEnableAction.Run();
      //Assert      
      disabledUser.IsApproved.Should().BeTrue();
      enabledUser.IsApproved.Should().BeTrue();
    }

    private MembershipUser GetUser(string userName, bool isApproved)
    {
      return new MembershipUser("fake", userName, null, "a@a.aa", "", "", isApproved, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
    }
  }
}
