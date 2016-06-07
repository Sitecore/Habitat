namespace Sitecore.Foundation.Dictionary.Tests
{
  using System.Web;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Dictionary.Repositories;
  using Sitecore.Foundation.Testing;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Mvc.Helpers;
  using Xunit;

  public class SitecoreExtensionsTests
  {
    private IDictionaryPhraseRepository dictionaryPhraseRepository;

    public SitecoreExtensionsTests()
    {
      this.dictionaryPhraseRepository = Substitute.For<IDictionaryPhraseRepository>();
      HttpContext.Current = HttpContextMockFactory.Create();
      HttpContext.Current.Items["DictionaryPhraseRepository.Current"] = this.dictionaryPhraseRepository;
    }

    [Theory]
    [AutoDbData]
    public void Dictionary_Call_ReturnTranslationByPath(string path, string defaultValue, string translate)
    {
      //Arrange
      this.dictionaryPhraseRepository.Get(path, defaultValue).Returns(translate);

      //Act
      var result = SitecoreExtensions.Dictionary(null, path, defaultValue);
      
      //Assert      
      result.Should().Be(translate);
    }

    [Theory]
    [AutoDbData]
    public void DictionaryField_NoTranslationItemExists_ReturnDefaultValue(string path, string defaultValue)
    {
      //Arrange
      this.dictionaryPhraseRepository.GetItem(path, defaultValue).Returns(x=>null);

      //Act
      var result = SitecoreExtensions.DictionaryField(null, path, defaultValue);

      //Assert      
      result.ToHtmlString().Should().Be(defaultValue);
    }

    [Theory]
    [AutoDbData]
    public void DictionaryField_TranslationItemExists_ReturnFieldRenderer(string path, string defaultValue, Item translateItem, [Substitute]SitecoreHelper sitecoreHelper)
    {
      //Arrange
      this.dictionaryPhraseRepository.GetItem(path, defaultValue).Returns(x => translateItem);

      sitecoreHelper.Field(Arg.Any<string>(), Arg.Any<Item>()).Returns(x => new HtmlString($"fieldId:{x.Arg<string>()} itemId:{x.Arg<Item>().ID}"));
      //Act
      var result = SitecoreExtensions.DictionaryField(sitecoreHelper, path, defaultValue);

      //Assert      
      result.ToHtmlString().Should().Be($"fieldId:{{DDACDD55-5B08-405F-9E58-04F09AED640A}} itemId:{translateItem.ID}");
    }
  }
}
