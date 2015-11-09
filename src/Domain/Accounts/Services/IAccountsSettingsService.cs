using System.Net.Mail;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Habitat.Accounts.Services
{
  public interface IAccountsSettingsService
  {
    string GetPageLink(Item contextItem, ID fieldID);
    MailMessage GetForgotPasswordMailTemplate();
  }
}