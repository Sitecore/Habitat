using System;
using System.Net.Mail;
using Habitat.Accounts.Texts;
using Sitecore;

namespace Habitat.Accounts.Services
{
  public class NotificationService : INotificationService
  {
    private readonly IAccountsSettingsService siteSettings;

    public NotificationService(IAccountsSettingsService siteSettings)
    {
      this.siteSettings = siteSettings;
    }

    public void SendPassword(string email, string newPassword)
    {
      var mail = siteSettings.GetForgotPasswordMailTemplate();
      mail.To.Add(email);
      mail.Body = mail.Body.Replace("$password$", newPassword);

      MainUtil.SendMail(mail);
    }
  }
}