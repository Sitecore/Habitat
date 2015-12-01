namespace Habitat.Framework.SitecoreExtensions.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using Habitat.Framework.SitecoreExtensions.Repositories;
  using Habitat.Framework.SitecoreExtensions.Tests.Common;
  using NSubstitute;
  using Sitecore.Mvc.Common;
  using Sitecore.Mvc.Presentation;
  using Xunit;
  using Ploeh.AutoFixture.AutoNSubstitute;

  public class RenderingPropertiesRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldInitObjectProperties()
    {
      var context = new RenderingContext();
      var rendering = new Rendering();
      var properties = new RenderingProperties(rendering);
      properties["Parameters"] = "property1=5&property2=10";
      context.Rendering = new Rendering() {Properties = properties};
      var repository = new RenderingPropertiesRepository();
      ContextService.Get().Push(context);
      var resultObject = repository.Get<Model>();
      resultObject.Property1.ShouldBeEquivalentTo(5);
      resultObject.Property2.ShouldBeEquivalentTo(10);
    }

    public class Model
    {
      public string Property1 { get; set; }

      public string Property2 { get; set; }
    }
  }
}
