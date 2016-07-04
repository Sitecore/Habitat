using System;
using System.Linq;
using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
using Sitecore.Foundation.Common.Specflow.UtfService;

namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  public static class LanguageProvider
  {
    public static void AddLanguage(LanguageModel language)
    {
      var itemName = ItemService.GetNameFromPath(language.ItemPath);
      ContextExtensions.UtfService.CreateItem(itemName, ItemService.LanguageRootPath, ItemService.LanguageTemplateId);
      ContextExtensions.UtfService.EditItem(language.ItemPath, "Charset", language.Charset);
      ContextExtensions.UtfService.EditItem(language.ItemPath, "Regional Iso Code", language.Charset);
      ContextExtensions.UtfService.EditItem(language.ItemPath, "Code page", language.CodePage);
      ContextExtensions.UtfService.EditItem(language.ItemPath, "Encoding", language.Encoding);
      ContextExtensions.UtfService.EditItem(language.ItemPath, "Iso", language.Iso);
    }

    public static bool LangaugeExists(string langaugePath)
    {
      var children = ContextExtensions.UtfService.GetChildren(ItemService.LanguageRootPath,true);
      return children.All(x => !x.Equals(langaugePath, StringComparison.InvariantCultureIgnoreCase));
    }
  }
}
