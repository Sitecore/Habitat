namespace Habitat.Person.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using FluentAssertions;
	using Habitat.Framework.Indexing.Models;
	using Habitat.Person.Indexing;
	using Sitecore.Data;
	using Xunit;

	public class PersonIndexContentProviderTests
	{
		[Theory]
		public void SupportedTemplatesShouldReturnAtLeastOneTemplate(IQuery query)
		{
			var provider = new PersonIndexContentProvider();
			var templates = provider.SupportedTemplates;
			var supportedTemplates = templates as ID[] ?? templates.ToArray();
			supportedTemplates.Should().NotBeNull();
			supportedTemplates.Should().HaveCount(c => c > 0);
		}

		[Theory]
		public void SupportedTemplatesShouldOnlyReturnTemplatesInModule(IQuery query)
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