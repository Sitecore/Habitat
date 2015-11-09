using System;
using System.Net.Mail;
using Habitat.Accounts.Texts;
using Sitecore;

namespace Habitat.Accounts.Services
{
  public class NotificationService : INotificationService
  {
    public void SendPassword(string email, string newPassword)
    {
      var emailMessage = new MailMessage("noreply@", email, Captions.ResetPassword, newPassword);
      MainUtil.SendMail(emailMessage);

      return;
    }
  }
}