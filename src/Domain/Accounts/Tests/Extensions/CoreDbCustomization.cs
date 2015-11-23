namespace Habitat.Accounts.Tests.Extensions
{
  using System.Linq;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.Dsl;
  using Sitecore.FakeDb;

  public class CoreDbCustomization : ICustomization
  {
    public void Customize(IFixture fixture)
    {

      foreach (var customization in fixture.Customizations.Where(c => c is NodeComposer<Db>))
      {
        fixture.Customizations.Remove(customization);
      }

      fixture.Inject(new Db("core"));
    }
  }
}