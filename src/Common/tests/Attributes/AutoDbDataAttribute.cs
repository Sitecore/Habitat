namespace UnitTests.Common.Attributes
{
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Kernel;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.FakeDb.AutoFixture;
  using UnitTests.Common.Builders;
  using UnitTests.Common.Commands;

  public class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
    {
      Fixture.Customize(new AutoDbCustomization());
      Fixture.Customize(new AutoNSubstituteCustomization());
      Fixture.Customizations.Add(new Postprocessor(new ContentAttributeRelay(), new AddContentDbItemsCommand()));
      Fixture.Customizations.Add(new Postprocessor(new AttributeRelay<ReplaceSearchProviderAttribute>(), new ReplaceSearchAttributeCommand()));
      Fixture.Customizations.Add(new ResolvePipelineSpecimenBuilder());
    }
  }
}