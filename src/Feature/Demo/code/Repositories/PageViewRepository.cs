﻿namespace Sitecore.Feature.Demo.Repositories
{
    using System;
    using System.Linq;
    using Sitecore.Analytics.Core;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Dictionary.Repositories;

    [Service(typeof(IPageViewRepository))]
    public class PageViewRepository : IPageViewRepository
    {
        public PageView Get(ICurrentPageContext pageContext)
        {
            return new PageView
            {
                Duration = this.GetDuration(pageContext),
                HasEngagementValue = pageContext.PageEvents.Any(pe => pe.Value > 0),
                HasMvTest = HasMvTest(pageContext),
                HasPersonalisation = HasPersonalisation(pageContext),
                Path = this.GetCleanPath(pageContext),
                FullPath = this.GetFullPath(pageContext)
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

        private string GetFullPath(IPage page)
        {
            var pageName = RemoveLanguage(page).Replace("//", "/").Remove(0, 1).Replace(".aspx", "");
            if (pageName == string.Empty || this.IsLanguage(pageName))
            {
                pageName = DictionaryPhraseRepository.Current.Get("/Demo/PageView/Home", "Home");
            }
            return pageName;
        }

        private string GetCleanPath(IPage page)
        {
            var pageName = this.GetFullPath(page);
            if (pageName.Contains("/"))
            {
                pageName = "..." + pageName.Substring(pageName.LastIndexOf("/", StringComparison.Ordinal));
            }
            return pageName.Length < 21 ? $"{pageName}" : $"{pageName.Substring(0, 20)}...";
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