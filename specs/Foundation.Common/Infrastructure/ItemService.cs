using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  public static class ItemService
  {
    public const string LanguageRootPath = "/sitecore/system/Languages";

    public static string GetNameFromPath(string path)
    {
      return path.Split('/').Last();
    }

    public const string LanguageTemplateId = "/sitecore/templates/System/Language";


  }
}
