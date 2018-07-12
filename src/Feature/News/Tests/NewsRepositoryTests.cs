namespace Sitecore.Feature.News.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using NSubstitute;
    using Ploeh.AutoFixture.AutoNSubstitute;
    using Ploeh.AutoFixture.Xunit2;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb;
    using Sitecore.Feature.News.Repositories;
    using Sitecore.Feature.News.Tests.Extensions;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.Indexing.Repositories;
    using Sitecore.Foundation.Indexing.Services;
    using Xunit;

    public class NewsRepositoryTests
    {
        [Theory]
        [AutoDbData]
        public void Get_NullAs1Parameter_ThrowArgumentNullException([Frozen] ISearchServiceRepository searchServiceRepository, [Frozen] ISearchSettings searchSettings)
        {
            var repo = new NewsRepository(searchServiceRepository);
            Action act = () => repo.Get(null);
            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [AutoDbData]
        public void Get_ItemNotDerivedFromNewsFolderTemplate_ThrowArgumentNullException([Frozen] ISearchServiceRepository searchServiceRepository, [Frozen] ISearchSettings searchSettings, Item contextItem)
        {
            var repo = new NewsRepository(searchServiceRepository);
            Action act = () => repo.Get(contextItem);
            act.Should().Throw<ArgumentException>();
        }
    }
}