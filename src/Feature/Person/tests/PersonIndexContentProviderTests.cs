namespace Sitecore.Feature.Person.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using FluentAssertions;
	using Sitecore.Foundation.Indexing.Models;
	using Sitecore.Feature.Person.Indexing;
	using Sitecore.Data;
	using Xunit;

	public class PersonIndexContentProviderTests
	{
		[Fact]
		public void SupportedTemplatesShouldReturnAtLeastOneTemplate()
		{
			var provider = new PersonIndexContentProvider();
			var templates = provider.SupportedTemplates;
			var supportedTemplates = templates as ID[] ?? templates.ToArray();
			supportedTemplates.Should().NotBeNull();
			supportedTemplates.Should().HaveCount(c => c > 0);
		}

		[Fact]
		public void SupportedTemplatesShouldOnlyReturnTemplatesInModule()
		{
			var provider = new PersonIndexContentProvider();
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
	}
}