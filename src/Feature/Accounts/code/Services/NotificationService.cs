namespace Sitecore.Feature.Accounts.Services
{
  public class NotificationService : INotificationService
  {
    private readonly AccountsSettingsService siteSettings;

    public NotificationService(AccountsSettingsService siteSettings)
    {
      this.siteSettings = siteSettings;
    }

    public void SendPassword(string email, string newPassword)
    {
      var mail = this.siteSettings.GetForgotPasswordMailTemplate();
      mail.To.Add(email);
      mail.Body = mail.Body.Replace("$password$", newPassword);

      MainUtil.SendMail(mail);
    }
  }
}