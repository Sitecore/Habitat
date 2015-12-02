namespace Sitecore.Framework.SitecoreExtensions.Tests.Common
{
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.FakeDb.AutoFixture;

  internal class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
      : base(new Fixture().Customize(new AutoDbCustomization()))
    {
      Fixture.Customizations.Add(new AutoNSubstituteCustomization().Builder);

    }
  }
}