using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;
using Sitecore.FakeDb.AutoFixture;

namespace Habitat.Accounts.Tests
{
  internal class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
      : base(new Fixture().Customize(new AutoDbCustomization()))
    {
      this.Fixture.Customizations.Add(new AutoMoqCustomization().Relay);
    }
  }
}