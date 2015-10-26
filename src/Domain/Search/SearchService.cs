using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Habitat.Search.Models;
using Lucene.Net.Search;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Search;
using SearchResult = Habitat.Search.Models.SearchResult;

namespace Habitat.Search
{
    //public class SearchService
    //{
    //    public SearchService(Item searchConfiguration)
    //    {
    //        Configuration = searchConfiguration;
    //    }

    //    public SearchResults Search(string searchString)
    //    {
    //        using (var context = ContentSearchManager.GetIndex(Indexname).CreateSearchContext())
    //        {                
    //            var words = searchString.Split(new[] {' ', '.', ',', '\t', '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
    //            if (!words.Any())
    //                words = new[] {"*"};

    //            var root = GetSearchRoot();
    //            var query = context.GetQueryable<SearchResult>().Where(item => item.Path.StartsWith(root.Paths.FullPath));
    //            query = words.Aggregate(query, (current, word) => current.Where(item => item.Title.Contains(word) || item.Content.Contains(word)));
    //            query = query.Filter(item => item.Language == Sitecore.Context.Language.Name);
    //            var results = query.FacetOn(item => item.Tags).FacetOn(item => item.TemplateName).GetResults();

    //            return new SearchResults()
    //            {
    //                ConfigurationItem = Configuration,
    //                Query = searchString,
    //                Results = results.Hits.Select(h => CreateSearchResult(h.Document)).ToArray()
                    
    //            };
    //        }
    //    }

    //    private SearchResult CreateSearchResult(SearchResult document)
    //    {
    //        //TODO: Determine title and description
    //        document.ContentType = GetContentType(document.TemplateId);
    //        return document;
    //    }

    //    private string GetContentType(ID templateID)
    //    {
    //        var templateItem = Configuration.Database.GetItem(templateID);
    //        if (templateItem == null)
    //            return null;
    //        return string.IsNullOrWhiteSpace(templateItem.DisplayName) ? templateItem.Name : templateItem.DisplayName;
    //    }

    //    private Item GetSearchRoot() { 
    //        //TODO: Be more intelligent, allow editors to override
    //        return Sitecore.Context.Site.GetRoot();
    //    }

    //    private static string Indexname
    //    {
    //        get
    //        {
    //            //TODO: Be more intelligent, read from <site>...
    //            return "sitecore_master_index";
    //        }
    //    }

    //    public Item Configuration { get; set; }
    //}
}
