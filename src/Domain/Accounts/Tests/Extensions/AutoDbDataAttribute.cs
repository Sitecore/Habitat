namespace Habitat.Accounts.Tests.Extensions
{
  using System.Web.Security;
  using NSubstitute;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Security.Accounts;

  internal class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
      : base(new Fixture().Customize(new AutoDbCustomization()))
    {
      this.Fixture.Customizations.Add(new AutoNSubstituteCustomization().Builder);
      this.Fixture.Register(() =>
      {
        var user = Substitute.ForPartsOf<FakeMembershipUser>();
        user.ProviderName.Returns("fake");
        return user;
      });

      this.Fixture.Register(() => Substitute.For<MembershipProvider>());
    }
  }
}