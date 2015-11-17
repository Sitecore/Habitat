namespace Habitat.Accounts.Tests.Extensions
{
  using System.Net.Mail;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;

  internal class AutoEmailDataAttribute : AutoDataAttribute
  {
    public AutoEmailDataAttribute()
      : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
      this.Fixture.Register(() =>
        new MailMessage(this.Fixture.Create<MailAddress>().Address,
          this.Fixture.Create<MailAddress>().Address,
          this.Fixture.Create("subject"),
          this.Fixture.Create("body")));
    }
  }
}