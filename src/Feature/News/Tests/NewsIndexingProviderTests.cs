namespace Sitecore.Feature.News.Tests
{
    using FluentAssertions;
    using NSubstitute;
    using Sitecore.Feature.News.Tests.Extensions;
    using Xunit;
    using Sitecore.Collections;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb.AutoFixture;
    using Sitecore.FakeDb.Sites;
    using Sitecore.Feature.News.Indexing;
    using Sitecore.Foundation.Indexing.Models;

    public class NewsIndexingProviderTests
  {
    [Theory]
    [AutoDbData]
    public void ContentType_ShouldBeNews(NewsIndexingProvider provider, [Content] Item dictionaryRoot)
    {
      Context.Site = new FakeSiteContext(new StringDictionary()
      {
        ["dictionaryPath"] = dictionaryRoot.Paths.FullPath,
        ["database"] = "master"
      });
      provider.ContentType.Should().Be("News");
    }

    [Theory]
    [AutoDbData]
    public void SupportedTemplates_ContainsNewsArticleTemplate(NewsIndexingProvider provider)
    {
      provider.SupportedTemplates.Should().Contain(Templates.NewsArticle.ID);
    }

    [Theory]
    [AutoDbData]
    public void GetQueryPredicate_WrongTemplate_ShouldReturnFalse(NewsIndexingProvider provider, IQuery query)
    {
      provider.GetQueryPredicate(query).Compile().Invoke(new SearchResultItem()).Should().BeFalse();
    }

    [Theory]
    [InlineAutoDbData(Templates.NewsArticle.Fields.Title_FieldName)]
    [InlineAutoDbData(Templates.NewsArticle.Fields.Body_FieldName)]
    [InlineAutoDbData(Templates.NewsArticle.Fields.Title_FieldName)]
    public void GetQueryPredicate_NewsItemWithWrongContent_ShouldReturnFalse(string fieldName, NewsIndexingProvider provider, IQuery query, string queryText, string contentText)
    {
      var item = Substitute.For<SearchResultItem>();
      query.QueryText.Returns(queryText);
      item[fieldName].Returns(contentText);
      provider.GetQueryPredicate(query).Compile().Invoke(item).Should().BeFalse();
    }
  }
}
