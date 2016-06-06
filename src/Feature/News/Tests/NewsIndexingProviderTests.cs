namespace Sitecore.Feature.News.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Sitecore.FakeDb;
  using Sitecore.Feature.News.Repositories;
  using Sitecore.Feature.News.Tests.Extensions;
  using Sitecore.Foundation.Indexing;
  using Sitecore.Rules.Conditions.ItemConditions;
  using Xunit;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Data.Items;
  using Sitecore.Feature.News.Indexing;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.Indexing.Repositories;
  using Sitecore.Search;

  public class NewsIndexingProviderTests
  {
    [Theory]
    [AutoDbData]
    public void ContentType_ShouldBeNews(NewsIndexingProvider provider)
    {
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
    public void GetQueryPredicate_NewsItemWithSearchContent_ShouldReturnTrue(string fieldName, NewsIndexingProvider provider, IQuery query, string queryText)
    {
      var item = Substitute.For<SearchResultItem>();
      query.QueryText.Returns(queryText);
      item[fieldName].Returns(queryText);
      provider.GetQueryPredicate(query).Compile().Invoke(item).Should().BeTrue();
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
