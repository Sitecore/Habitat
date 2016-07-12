using System.Linq;

namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  public static class ItemService
  {
    public const string LanguageRootPath = "/sitecore/system/Languages";
    public const string RenderingsRootPath = "/sitecore/layout/Renderings";
    public const string WebFormsRootPath = "/sitecore/layout/Renderings/Modules/Web Forms for Marketers/";

    public static string GetNameFromPath(string path)
    {
      return path.Split('/').Last();
    }

    public const string LanguageTemplateId = "/sitecore/templates/System/Language";

    

    public static string GetModuleRenderingPath(string module, string renderingName)
    {
      return $"{RenderingsRootPath}/{module}/{renderingName}";
    }

    public static void AddItem(ItemFieldDefinition item)
    {
      ContextExtensions.UtfService.CreateItem(item.ItemPath, item.ParentItemPath, item.TemplateItemPath);
    }

    public static bool ItemExists(ItemFieldDefinition item)
    {
      return !string.IsNullOrEmpty(ContextExtensions.UtfService.ItemExistsByPath(item.ItemPath));
    }
  }
}
