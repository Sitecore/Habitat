namespace Sitecore.Feature.News.Tests.Extensions
{
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.FakeDb.AutoFixture;

  public class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
      : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
      this.Fixture.Customize(new AutoDbCustomization());
    }
  }

  public class InlineAutoDbDataAttribute : InlineAutoDataAttribute
  {
    public InlineAutoDbDataAttribute(params object[] @params)
      : base(new AutoDbDataAttribute(), @params)
    {
    }
  }
}