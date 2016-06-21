using System;
using System.Linq;
using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Service_References.UtfService;

namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  public static class LanguageProvider
  {
    public static void AddLanguage(LanguageModel language)
    {
      var itemName = ItemService.GetNameFromPath(language.ItemPath);
      ContextExtensions.UtfService.CreateItem(itemName, ItemService.LanguageRootPath, ItemService.LanguageTemplateId, BaseSettings.UserName, BaseSettings.Password, Database.Master);

      ContextExtensions.UtfService.EditItem(language.ItemPath, "Charset", language.Charset, BaseSettings.UserName,
        BaseSettings.Password, Database.Master);
      ContextExtensions.UtfService.EditItem(language.ItemPath, "Regional Iso Code", language.Charset, BaseSettings.UserName,
        BaseSettings.Password, Database.Master);
      ContextExtensions.UtfService.EditItem(language.ItemPath, "Code page", language.CodePage, BaseSettings.UserName,
        BaseSettings.Password, Database.Master);
      ContextExtensions.UtfService.EditItem(language.ItemPath, "Encoding", language.Encoding, BaseSettings.UserName,
        BaseSettings.Password, Database.Master);
      ContextExtensions.UtfService.EditItem(language.ItemPath, "Iso", language.Iso, BaseSettings.UserName,
        BaseSettings.Password, Database.Master);
    }

    public static bool LangaugeExists(string langaugePath)
    {
      var children = ContextExtensions.UtfService.GetChildren(ItemService.LanguageRootPath, Database.Master, true);
      return children.All(x => !x.Equals(langaugePath, StringComparison.InvariantCultureIgnoreCase));
    }
  }
}
