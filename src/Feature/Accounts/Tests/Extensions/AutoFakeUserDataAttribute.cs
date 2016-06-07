namespace Sitecore.Feature.Accounts.Tests.Extensions
{
  using NSubstitute;
  using Ploeh.AutoFixture;
  using Sitecore.FakeDb.Security.Accounts;
  using Sitecore.Foundation.Testing.Attributes;

  public class AutoFakeUserDataAttribute: AutoDbDataAttribute
  {
    public AutoFakeUserDataAttribute()
    {
      this.Fixture.Register(() =>
      {
        var user = Substitute.ForPartsOf<FakeMembershipUser>();
        user.ProviderName.Returns("fake");
        return user;
      });
    }
  }
}
