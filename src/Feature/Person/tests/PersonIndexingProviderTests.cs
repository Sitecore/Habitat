namespace Sitecore.Feature.Person.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using FluentAssertions;
	using NSubstitute;
	using Sitecore.Collections;
	using Sitecore.ContentSearch.SearchTypes;
	using Sitecore.Foundation.Indexing.Models;
	using Sitecore.Feature.Person.Indexing;
	using Sitecore.Data;
	using Sitecore.Data.Items;
	using Sitecore.FakeDb.AutoFixture;
	using Sitecore.FakeDb.Sites;
	using Sitecore.Foundation.Testing.Attributes;
	using Xunit;


  public class PersonIndexingProviderTests
	{
		[Fact]
		public void SupportedTemplatesShouldReturnAtLeastOneTemplate()
		{
			var provider = new PersonIndexingProvider();
			var templates = provider.SupportedTemplates;
			var supportedTemplates = templates as ID[] ?? templates.ToArray();
			supportedTemplates.Should().NotBeNull();
			supportedTemplates.Should().HaveCount(c => c > 0);
		}

		[Fact]
		public void SupportedTemplatesShouldOnlyReturnTemplatesInModule()
		{
			var provider = new PersonIndexingProvider();
			var templates = provider.SupportedTemplates;
			var supportedTemplates = templates as ID[] ?? templates.ToArray();
			supportedTemplates.Should().NotBeNull();
			var templatesInModule = GetTemplatesInModule();
			supportedTemplates.Should().OnlyContain(t => templatesInModule.Contains(t));
		}

		private static IEnumerable<ID> GetTemplatesInModule()
		{
			return typeof(Templates).GetNestedTypes().Select(nt => nt.GetField("ID")).Where(f => f != null && f.FieldType == typeof(ID) && f.IsStatic).Select(f => (ID)f.GetValue(null)).ToArray();
		}


    [Theory]
    [AutoDbData]
    public void ContentType_ShouldBeEmployee(PersonIndexingProvider provider, [Content] Item dictionaryRoot)
    {
      Context.Site = new FakeSiteContext(new StringDictionary()
      {
        ["dictionaryPath"] = dictionaryRoot.Paths.FullPath,
        ["database"] = "master"
      });
      provider.ContentType.Should().Be("Employee");
    }

    [Theory]
    [AutoDbData]
    public void SupportedTemplates_ContainsNewsArticleTemplate(PersonIndexingProvider provider)
    {
      provider.SupportedTemplates.Should().Contain(Templates.Employee.ID);
    }

    [Theory]
    [AutoDbData]
    public void GetQueryPredicate_WrongTemplate_ShouldReturnFalse(PersonIndexingProvider provider, IQuery query)
    {
      provider.GetQueryPredicate(query).Compile().Invoke(new SearchResultItem()).Should().BeFalse();
    }

    [Theory]
    [InlineAutoDbData("Summary")]
    [InlineAutoDbData("Name")]
    [InlineAutoDbData("Title")]
    public void GetQueryPredicate_PersonItemWithWrongContent_ShouldReturnFalse(string fieldName, PersonIndexingProvider provider, IQuery query, string queryText, string contentText)
    {
      var item = Substitute.For<SearchResultItem>();
      query.QueryText.Returns(queryText);
      item[fieldName].Returns(contentText);
      provider.GetQueryPredicate(query).Compile().Invoke(item).Should().BeFalse();
    }
  }
}
