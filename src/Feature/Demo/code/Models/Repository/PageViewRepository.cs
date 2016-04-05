namespace Sitecore.Feature.Demo.Models.Repository
{
  using System;
  using System.Linq;
  using Sitecore.Analytics.Core;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Foundation.Dictionary.Repositories;

  internal class PageViewRepository
  {
    public PageView Get(ICurrentPageContext pageContext)
    {
      return new PageView
             {
               Duration = GetDuration(pageContext),
               HasEngagementValue = pageContext.PageEvents.Any(pe => pe.Value > 0),
               HasMvTest = HasMvTest(pageContext),
               HasPersonalisation = HasPersonalisation(pageContext),
               Path = GetCleanPath(pageContext)
             };
    }

    private static bool HasPersonalisation(ICurrentPageContext pageContext)
    {
      return pageContext.Personalization != null && pageContext.Personalization.ExposedRules != null && pageContext.Personalization.ExposedRules.Any();
    }

    private static bool HasMvTest(ICurrentPageContext pageContext)
    {
      return pageContext.MvTest != null && !pageContext.MvTest.IsSuspended && pageContext.MvTest.EligibleRules != null && pageContext.MvTest.EligibleRules.Any();
    }

    private TimeSpan GetDuration(ICurrentPageContext pageContext)
    {
      return TimeSpan.FromMilliseconds(pageContext.Duration);
    }

    private string GetCleanPath(IPage page)
    {
      var pageName = RemoveLanguage(page).Replace("//", "/").Remove(0, 1).Replace(".aspx", "");
      if (pageName == string.Empty || IsLanguage(pageName))
      {
        pageName = DictionaryRepository.Get("/Demo/PageView/Home", "Home");
      }
      if (pageName.Contains("/"))
      {
        pageName = "..." + pageName.Substring(pageName.LastIndexOf("/", StringComparison.Ordinal));
      }
      return pageName.Length < 27 ? $"{pageName}" : $"{pageName.Substring(0, 26)}...";
    }

    private bool IsLanguage(string pageName)
    {
      //TODO: support other languages
      return pageName == "en";
    }

    private static string RemoveLanguage(IPage page)
    {
      //TODO: support other languages
      return page.Url.Path.Replace("/en", "/");
    }
  }
}