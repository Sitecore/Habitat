namespace Sitecore.Feature.Accounts.Tests.Extensions
{
  using System.Collections.Generic;
  using Ploeh.AutoFixture;

  public class RightKeysCustomization<TKey,TValue> : ICustomization
  {
    private readonly TKey[] keys;

    public RightKeysCustomization(params TKey[] keys)
    {
      this.keys = keys;
    }

    public void Customize(IFixture fixture)
    {
      fixture.Freeze<IDictionary<TKey, TValue>>(c => c.FromFactory(() =>
      {
        var d = new Dictionary<TKey, TValue>();
        foreach (var key in this.keys)
        {
          d.Add(key, fixture.Create<TValue>());
        }

        return d;
      }));
    }
  }
}
