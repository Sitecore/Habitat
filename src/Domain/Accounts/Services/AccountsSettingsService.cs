using System;
using System.Net.Mail;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Habitat.Accounts.Services
{
  public class AccountsSettingsService : IAccountsSettingsService
  {
    public string GetPageLink(Item contextItem,ID fieldID)
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
        item = contextItem.GetAncestorOrSelfOfTemplate(Templates.AccountsSettings.ID);
      item = item ?? Sitecore.Context.Site.GetContextItem(Templates.AccountsSettings.ID);

      return item;
    }

    public MailMessage GetForgotPasswordMailTemplate()
    {
     return new MailMessage();
    }
  }
}