namespace Habitat.Accounts.Tests.FixtureContext
{
  using System;
  using netDumbster.smtp;
  using Sitecore.Configuration;

  public class SmptWrapper : IDisposable
  {
    public static readonly Random rnd = new Random();


    public SmptWrapper()
    {
      var port = rnd.Next(50000, 60000);
      this.settingsSwitcher = new SettingsSwitcher("MailServerPort", port.ToString());

      this.SmtpServerInstance = SimpleSmtpServer.Start(Settings.MailServerPort);
    }

    public SimpleSmtpServer SmtpServerInstance { get; set; }

    public void Dispose()
    {
      if (this.SmtpServerInstance != null)
      {
        this.SmtpServerInstance.Stop();
      }
      if (this.settingsSwitcher != null)
      {
        this.settingsSwitcher.Dispose();
      }
    }


    private readonly SettingsSwitcher settingsSwitcher;
  }
}