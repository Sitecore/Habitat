using System.Net.Mail;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;

namespace Habitat.Accounts.Tests.Extensions
{
  internal class AutoEmailDataAttribute : AutoDataAttribute
  {
    public AutoEmailDataAttribute()
      : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
      
      this.Fixture.Register<MailMessage>(() => 
        new MailMessage(this.Fixture.Create<MailAddress>().Address,
          this.Fixture.Create<MailAddress>().Address, 
          this.Fixture.Create<string>("subject"),
          this.Fixture.Create<string>("body")));
    }
  }
}