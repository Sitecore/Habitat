namespace Sitecore.Feature.Accounts.Tests
{
  using System.Linq;
  using System.Net.Mail;
  using FluentAssertions;
  using netDumbster.smtp;
  using NSubstitute;
  using Sitecore.Feature.Accounts.Services;
  using Sitecore.Feature.Accounts.Tests.Extensions;
  using Sitecore.Feature.Accounts.Tests.FixtureContext;
  using Xunit;

  public class NotificationServiceTests : IClassFixture<SmptWrapper>
  {
    public NotificationServiceTests(SmptWrapper fixture)
    {
      this.SmtpServer = fixture.SmtpServerInstance;
      this.SmtpServer.ClearReceivedEmail();
    }

    public SimpleSmtpServer SmtpServer { get; set; }

    [Theory]
    [AutoEmailData]
    public void SendPasswordShouldSendOnlyOneEmail(IAccountsSettingsService settings, MailMessage msg)
    {
      this.SentMail(settings, msg);
      this.SmtpServer.ReceivedEmailCount.Should().Be(1);
    }
    
    [Theory]
    [AutoEmailData]
    public void SendPasswordShouldUseSubjectFromSettings(IAccountsSettingsService settings, MailMessage msg)
    {
      var sentMail = this.SentMail(settings, msg);
      sentMail.Headers["Subject"].Should().NotBeNullOrWhiteSpace();
      sentMail.Headers["Subject"].Should().BeEquivalentTo(settings.GetForgotPasswordMailTemplate().Subject);
    }

    private SmtpMessage SentMail(IAccountsSettingsService settings, MailMessage msg, string to = "fake@sc.net")
    {
      settings.GetForgotPasswordMailTemplate().Returns(msg);
      var ns = new NotificationService(settings);
      ns.SendPassword(to, "expectedpassword");
      var sentMail = this.SmtpServer.ReceivedEmail.Last();
      return sentMail;
    }

    [Theory]
    [AutoEmailData]
    public void SendPasswordShouldReplacePasswordKeyWithNewPassword(IAccountsSettingsService settings, MailMessage msg)
    {
      msg.Body = "fake body $password$";
      var sentMail = this.SentMail(settings, msg);
      sentMail.MessageParts.First().BodyData.Should().BeEquivalentTo("fake body expectedpassword");
    }

    [Theory]
    [AutoEmailData]
    public void SendPasswordShouldIgnoreOtherKeysToInsertPassword(IAccountsSettingsService settings, MailMessage msg)
    {
      msg.Body = "fake body $password2$";
      var sentMail = this.SentMail(settings, msg);
      sentMail.MessageParts.First().BodyData.Should().BeEquivalentTo("fake body $password2$");
    }

    [Theory]
    [AutoEmailData]
    public void SendPasswordShouldUseSourceAddressFromSettings(IAccountsSettingsService settings, MailMessage msg)
    {
      var sentMail = this.SentMail(settings, msg);
      sentMail.FromAddress.Address.Should().BeEquivalentTo(msg.From.Address);
    }

    [Theory]
    [AutoEmailData]
    public void SendPasswordShouldSetAddressTo(IAccountsSettingsService settings, MailMessage msg)
    {
      msg.To.Clear();
      var sentMail = this.SentMail(settings, msg, "fake@sitecore.net");
      sentMail.ToAddresses.First().Address.Should().Be("fake@sitecore.net");
    }
  }
}