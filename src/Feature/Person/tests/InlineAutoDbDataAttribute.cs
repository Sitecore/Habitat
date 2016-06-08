namespace Sitecore.Feature.Person.Tests
{
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Foundation.Testing.Attributes;

  public class InlineAutoDbDataAttribute : InlineAutoDataAttribute
  {
    public InlineAutoDbDataAttribute(params object[] @params)
      : base(new AutoDbDataAttribute(), @params)
    {
    }
  }
}