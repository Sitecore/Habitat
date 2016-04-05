namespace Sitecore.Foundation.Dictionary
{
  using System;
  using System.Web;
  using Sitecore.Foundation.Dictionary.Repositories;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Mvc.Helpers;

  public static class HtmlHelperExtensions
  {
    public static string Dictionary(this SitecoreHelper helper, string relativePath, string defaultValue = "")
    {
      return DictionaryRepository.Get(relativePath, defaultValue);
    }
  }
}