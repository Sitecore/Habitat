namespace Sitecore.Foundation.Testing.Attributes
{
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Kernel;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.Testing.Builders;
  using Sitecore.Foundation.Testing.Commands;

  public class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
    {
      Fixture.Customize(new AutoDbCustomization());
      Fixture.Customize(new AutoNSubstituteCustomization());
      Fixture.Customizations.Add(new Postprocessor(new ContentAttributeRelay(), new AddContentDbItemsCommand()));
      Fixture.Customizations.Add(new Postprocessor(new AttributeRelay<ReplaceSearchProviderAttribute>(), new ReplaceSearchAttributeCommand()));
      Fixture.Customizations.Add(new ResolvePipelineSpecimenBuilder());
      Fixture.Customizations.Insert(0, new RegisterViewToEngineBuilder());
      Fixture.Customizations.Add(new HtmlHelperBuilder());
      Fixture.Customizations.Add(new HttpContextBuilder());
    }
  }
}