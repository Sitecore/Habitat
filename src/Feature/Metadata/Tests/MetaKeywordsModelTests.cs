namespace Sitecore.Feature.Metadata.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using Sitecore.Feature.Metadata.Models;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class MetaKeywordsModelTests
  {
    [Theory]
    [AutoDbData]
    public void ToStrign_ShouldReturnCommaSeparatedListOfKeywords(List<string> keywords, MetaKeywordsModel model)
    {
      model.Keywords = keywords;
      var result = model.ToString();
      var keywordCollection = result.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
      var intersection = keywordCollection.Except(keywords);
      intersection.Count().Should().Be(0);
    }
  }
}