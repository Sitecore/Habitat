namespace Sitecore.Foundation.Dictionary.Tests.Repositories
{
  using System.Configuration;
  using FluentAssertions;
  using Sitecore.Collections;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Foundation.Dictionary.Repositories;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Sites;
  using Xunit;

  public class DictionaryRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Get_NotConfiguredPath_ThrowConfigurationErrorException(FakeSiteContext siteContext, DictionaryRepository repository)
    {
      repository.Invoking(x => x.Get(siteContext)).Should().Throw<ConfigurationErrorsException>();
    }

    [Theory]
    [AutoDbData]
    public void Get_NotDictionaryRootItem_ThrowConfigurationErrorException(Db db, DictionaryRepository repository)
    {
      //Arrange
      var siteContext = new FakeSiteContext(new StringDictionary()
      {
        ["dictionaryPath"] = "/sitecore/content/dictionaryPath",
        ["database"] = "master"
      });

      //Assert
      using (new SiteContextSwitcher(siteContext))
      {
        repository.Invoking(x => x.Get(siteContext)).Should().Throw<ConfigurationErrorsException>();
      }
    }

    [Theory]
    [AutoDbData]
    public void Get_DictionaryRootItemExists_ThrowConfigurationErrorException(Db db, [Content]DbItem item, DictionaryRepository repository)
    {
      //Arrange
      var siteContext = new FakeSiteContext(new StringDictionary()
      {
        ["dictionaryPath"] = item.FullPath,
        ["database"] = "master"
      });

      //Assert
      using (new SiteContextSwitcher(siteContext))
      {
        repository.Get(siteContext).Root.ID.Should().Be(item.ID);
      }
    }

    [Theory]
    [AutoDbData]
    public void Get_AutocreateNotSet_ReturnFalse([Content]DbItem item, DictionaryRepository repository)
    {
      //Arrange
      var siteContext = new FakeSiteContext(new StringDictionary()
      {
        ["dictionaryPath"] = item.FullPath,
        ["database"] = "master"
      });
      
      using (new SiteContextSwitcher(siteContext))
      {
        //Act
        var result = repository.Get(siteContext);

        //Assert
        result.AutoCreate.Should().BeFalse();
      }
    }

    [Theory]
    [AutoDbData]
    public void Get_AutocreateIsFalse_ReturnFalse([Content]DbItem item, DictionaryRepository repository)
    {
      //Arrange
      var siteContext = new FakeSiteContext(new StringDictionary()
      {
        ["dictionaryPath"] = item.FullPath,
        ["dictionaryAutoCreate"] = "false",
        ["database"] = "master"
      });

      using (new SiteContextSwitcher(siteContext))
      {
        //Act
        var result = repository.Get(siteContext);

        //Assert
        result.AutoCreate.Should().BeFalse();
      }
    }

    [Theory]
    [AutoDbData]
    public void Get_AutocreateIsTrueAndMasterDatabase_ReturnTrue(Db db,[Content]DbItem item, DictionaryRepository repository)
    {
      //Arrange
      var siteContext = new FakeSiteContext(new StringDictionary()
      {
        ["dictionaryPath"] = item.FullPath,
        ["dictionaryAutoCreate"] = "true",
        ["database"] = "master"
      });

      using (new SiteContextSwitcher(siteContext))
      {
        //Act
        var result = repository.Get(siteContext);

        //Assert
        result.AutoCreate.Should().BeTrue();
      }
    }

    [Theory]
    [AutoDbData]
    public void Get_AutocreateIsTrueAndNotMasterDatabase_ReturnFalse([CoreDb]Db db, [Content]DbItem item, DictionaryRepository repository)
    {
      //Arrange
      var siteContext = new FakeSiteContext(new StringDictionary()
      {
        ["dictionaryPath"] = item.FullPath,
        ["dictionaryAutoCreate"] = "true",
        ["database"] = "core"
      });

      using (new SiteContextSwitcher(siteContext))
      {
        //Act
        var result = repository.Get(siteContext);

        //Assert
        result.AutoCreate.Should().BeFalse();
      }
    }
  }
}
