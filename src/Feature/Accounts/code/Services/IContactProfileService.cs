namespace Sitecore.Feature.Accounts.Services
{
  using Sitecore.Feature.Accounts.Models;

  public interface IContactProfileService
  {
    void SetPreferredEmail(string email);

    void SetPreferredPhoneNumber(string phoneNumber);

    void SetProfile(EditProfile profile);
  }
}