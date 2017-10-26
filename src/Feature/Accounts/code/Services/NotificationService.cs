namespace Sitecore.Feature.Accounts.Services
{
    using Sitecore.Foundation.DependencyInjection;

    [Service(typeof(INotificationService))]
    public class NotificationService : INotificationService
    {
        private readonly IAccountsSettingsService siteSettings;

        public NotificationService(IAccountsSettingsService siteSettings)
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