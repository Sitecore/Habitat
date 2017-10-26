namespace Sitecore.Feature.Accounts.Services
{
  public interface INotificationService
  {
    void SendPassword(string email, string newPassword);
  }
}