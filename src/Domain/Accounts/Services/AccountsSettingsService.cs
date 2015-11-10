namespace Habitat.Accounts.Services
{
  using System;
  using System.Net.Mail;
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Exceptions;

  public class AccountsSettingsService : IAccountsSettingsService
  {
    public static AccountsSettingsService Instance => new AccountsSettingsService();

    public string GetPageLink(Item contextItem, ID fieldID)
    {
      var item = GetSettingsItem(contextItem);

      if (item == null)
      {
        throw new Exception("Page with accounts settings isn't specified");
      }

      InternalLinkField link = item.Fields[fieldID];

      if (link.TargetItem == null)
      {
        throw new Exception($"{link.InnerField.Name} link isn't set");
      }

      return link.TargetItem.Url();
    }

    private static Item GetSettingsItem(Item contextItem)
    {
      Item item = null;

      if (contextItem != null)
      {
        item = contextItem.GetAncestorOrSelfOfTemplate(Templates.AccountsSettings.ID);
      }
      item = item ?? Context.Site.GetContextItem(Templates.AccountsSettings.ID);

      return item;
    }

    public MailMessage GetForgotPasswordMailTemplate()
    {
      var settingsItem = GetSettingsItem(null);
      InternalLinkField link = settingsItem.Fields[Templates.AccountsSettings.Fields.FogotPasswordMailTemplate];
      var mailTemplateItem = link.TargetItem;

      if (mailTemplateItem == null)
      {
        throw new ItemNullException($"Could not find mail template item with {link.TargetID} ID");
      }

      var fromMail = mailTemplateItem.Fields[Templates.MailTemplate.Fields.From];

      if (!fromMail.HasValue || string.IsNullOrEmpty(fromMail.Value))
      {
        throw new InvalidValueException("'From' field in mail template should be set");
      }

      var body = mailTemplateItem.Fields[Templates.MailTemplate.Fields.Body];
      var subject = mailTemplateItem.Fields[Templates.MailTemplate.Fields.Subject];

      return new MailMessage
      {
        From = new MailAddress(fromMail.Value),
        Body = body.Value,
        Subject = subject.Value
      };
    }
  }
}