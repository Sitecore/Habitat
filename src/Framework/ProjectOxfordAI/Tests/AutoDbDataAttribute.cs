namespace Habitat.Framework.ProjectOxfordAI.Tests
{

  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.FakeDb.AutoFixture;

  internal class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
      : base(new Fixture().Customize(new AutoDbCustomization()))
    {
    }
  }
}