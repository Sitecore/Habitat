namespace Sitecore.Feature.Demo.Tests
{
  using Sitecore.Data;
  using Sitecore.FakeDb;

  public class DemoContentItem : DbItem
  {
    public DemoContentItem(string name, ID id)
      : base(name, id, Templates.DemoContent.ID)
    {
    }
  }
}