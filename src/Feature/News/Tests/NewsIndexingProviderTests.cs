﻿namespace Sitecore.Feature.News.Tests
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
  using Sitecore.Collections;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Feature.News.Indexing;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.Indexing.Repositories;
  using Sitecore.Search;
  using Sitecore.Sites;

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
      provider.SupportedTemplates.Should().Contain(News.Templates.NewsArticle.ID);
    }

    [Theory]
    [AutoDbData]
    public void GetQueryPredicate_WrongTemplate_ShouldReturnFalse(NewsIndexingProvider provider, IQuery query)
    {
      provider.GetQueryPredicate(query).Compile().Invoke(new SearchResultItem()).Should().BeFalse();
    }

    [Theory]
    [InlineAutoDbData(News.Templates.NewsArticle.Fields.NewsTitle_FieldName)]
    [InlineAutoDbData(News.Templates.NewsArticle.Fields.NewsBody_FieldName)]
    [InlineAutoDbData(News.Templates.NewsArticle.Fields.NewsTitle_FieldName)]
    public void GetQueryPredicate_NewsItemWithWrongContent_ShouldReturnFalse(string fieldName, NewsIndexingProvider provider, IQuery query, string queryText, string contentText)
    {
      var item = Substitute.For<SearchResultItem>();
      query.QueryText.Returns(queryText);
      item[fieldName].Returns(contentText);
      provider.GetQueryPredicate(query).Compile().Invoke(item).Should().BeFalse();
    }
  }
}
