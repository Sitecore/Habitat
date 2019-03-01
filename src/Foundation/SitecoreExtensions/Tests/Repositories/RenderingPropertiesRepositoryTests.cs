namespace Sitecore.Foundation.SitecoreExtensions.Tests.Repositories
{
    using FluentAssertions;
    using Sitecore.Foundation.SitecoreExtensions.Repositories;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.Mvc.Presentation;
    using Xunit;

    public class RenderingPropertiesRepositoryTests
    {
        [Theory]
        [AutoDbData]
        public void ShouldInitObjectProperties()
        {
            var rendering = new Rendering();
            rendering.Properties = new RenderingProperties(rendering) {["Parameters"] = "property1=5&property2=10"};

            var repository = new RenderingPropertiesRepository();
            var resultObject = repository.Get<Model>(rendering);
            resultObject.Property1.Should().BeEquivalentTo("5");
            resultObject.Property2.Should().BeEquivalentTo("10");
        }

        public class Model
        {
            public string Property1 { get; set; }

            public string Property2 { get; set; }
        }
    }
}