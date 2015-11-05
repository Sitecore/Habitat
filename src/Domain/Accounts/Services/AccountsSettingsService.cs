using System;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Habitat.Accounts.Services
{
  public static class AccountsSettingsService
  {
    public static string GetPageLink(Item contextItem,ID fieldID)
    {
      var item = contextItem.GetAncestorOrSelfOfTemplate(Templates.AccountsSettings.ID)
                ?? Sitecore.Context.Site.GetContextItem(Templates.AccountsSettings.ID);

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
  }
}