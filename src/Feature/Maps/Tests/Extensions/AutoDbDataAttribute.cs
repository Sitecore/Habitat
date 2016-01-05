namespace Sitecore.Feature.Maps.Tests.Extensions
{
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;

  public class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
      : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
      Fixture.Customize(new FakeDb.AutoFixture.AutoDbCustomization());
    }
  }
}