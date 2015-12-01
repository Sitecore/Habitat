namespace Habitat.Accounts.Tests.Extensions
{
  using NSubstitute;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Security.Accounts;
  using Sitecore.FakeDb.Sites;

  public class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
      : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
      this.Fixture.Customize(new AutoDbCustomization());
    }
  }
}