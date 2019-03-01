namespace Sitecore.Feature.Language.Tests
{
    using System.Linq;
    using FluentAssertions;
    using Moq;
    using Sitecore.Abstractions;
    using Sitecore.Collections;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb;
    using Sitecore.FakeDb.AutoFixture;
    using Sitecore.FakeDb.Sites;
    using Sitecore.Feature.Language.Repositories;
    using Sitecore.Feature.Language.Tests.Extensions;
    using Sitecore.Foundation.Multisite;
    using Sitecore.Foundation.Multisite.Providers;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.Globalization;
    using Sitecore.Links;
    using Xunit;

    public class LanguageRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void GetActive_ShouldReturnLanguageModelForContextLanguage(Db db, Foundation.Multisite.SiteContext siteContext, BaseLinkManager linkManager)
    {
      // Arrange
      const string language = "en";
      var repository = new LanguageRepository(siteContext, linkManager);

      using (new LanguageSwitcher(language))
      {
        // Act
        var activeLanguage = repository.GetActive();

        // Assert
        activeLanguage.TwoLetterCode.Should().BeEquivalentTo(language);
      }
    }

    [Theory]
    [AutoLanguageDbData]
    public void GetSupportedLanguages_OneSelected_ShouldReturnSelected(Db db, [Content] DbTemplate template, DbItem item, string rootName, ISiteDefinitionsProvider siteProvider)
    {
      const string supportedLanguage = "en";
      const string url = "/lorem/";

      // Arrange    
      template.BaseIDs = new[]
      {
        Templates.Site.ID, Feature.Language.Templates.LanguageSettings.ID
      };

      var languageItem = new DbItem(supportedLanguage);
      db.Add(languageItem);

      var siteRootId = ID.NewID;
      var siteRootItem = new DbItem(rootName, siteRootId, template.ID)
      {
        new DbField(Feature.Language.Templates.LanguageSettings.Fields.SupportedLanguages)
        {
          {
              supportedLanguage, languageItem.ID.ToString()
          }
        }
      };

      siteRootItem.Add(item);
      db.Add(siteRootItem);
      var contextItem = db.GetItem(item.ID);

      var linkManager = new Mock<BaseLinkManager>();
      linkManager.Setup(x => x.GetItemUrl(contextItem, It.IsAny<UrlOptions>())).Returns(url);

      var siteContext = new Mock<Foundation.Multisite.SiteContext>(siteProvider);
      siteContext.Setup(x => x.GetSiteDefinition(contextItem)).Returns(new SiteDefinition
      {
          Item = db.GetItem(siteRootId)
      });
      var repository = new LanguageRepository(siteContext.Object, linkManager.Object);

      using (new ContextItemSwitcher(contextItem))
      {
        // Act
        var supportedLanguages = repository.GetSupportedLanguages();

        // Assert
        supportedLanguages.Count().Should().BeGreaterThan(0);
        supportedLanguages.First().TwoLetterCode.Should().Be(supportedLanguage);
        supportedLanguages.First().Url.Should().Be(url);
      }
    }

    [Theory]
    [AutoLanguageDbData]
    public void GetSupportedLanguages_NoneSelected_ShouldReturnEmptyList(Db db, [Content] DbTemplate template, DbItem item, string rootName, ISiteDefinitionsProvider siteProvider, BaseLinkManager linkManager)
    {
      // Arrange
      template.BaseIDs = new[]
      {
        Templates.Site.ID, Feature.Language.Templates.LanguageSettings.ID
      };

      var languageItem = new DbItem("en");
      db.Add(languageItem);
      
      var siteRootId = ID.NewID;
      var siteRootItem = new DbItem(rootName, siteRootId, template.ID)
      {
        new DbField(Feature.Language.Templates.LanguageSettings.Fields.SupportedLanguages)
        {
          {
            "en", ""
          }
        }
      };

      siteRootItem.Add(item);
      db.Add(siteRootItem);
      var contextItem = db.GetItem(item.ID);

      var siteContext = new Mock<Foundation.Multisite.SiteContext>(siteProvider);
      siteContext.Setup(x => x.GetSiteDefinition(contextItem)).Returns(new SiteDefinition
      {
          Item = db.GetItem(siteRootId)
      });
      var repository = new LanguageRepository(siteContext.Object, linkManager);

      var fakeSite = new FakeSiteContext(new StringDictionary()
      {
        { "name", "fake" },
        { "database", "master" },
        {"rootPath", siteRootItem.FullPath },
        { "hostName", "local" }
      });

      using (new FakeSiteContextSwitcher(fakeSite))
      {
        using (new ContextItemSwitcher(contextItem))
        {
          // Act
          var supportedLanguages = repository.GetSupportedLanguages();

          // Assert
          supportedLanguages.Count().Should().Be(0);
        }
      }
    }
  }
}