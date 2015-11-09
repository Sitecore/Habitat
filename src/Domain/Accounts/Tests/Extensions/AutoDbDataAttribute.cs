using System.Net.Mail;
using System.Web.Security;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Sitecore.FakeDb.AutoFixture;
using Sitecore.FakeDb.Security.Accounts;

namespace Habitat.Accounts.Tests.Extensions
{
  internal class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
      : base(new Fixture().Customize(new AutoDbCustomization()))
    {
      Fixture.Customizations.Add(new AutoNSubstituteCustomization().Builder);
      this.Fixture.Register<FakeMembershipUser>(() => 
      {
        var user = Substitute.ForPartsOf<FakeMembershipUser>();
        user.ProviderName.Returns("fake");
        return user;
      });

      this.Fixture.Register<MembershipProvider>(() => Substitute.For<MembershipProvider>());
    }
  }
}