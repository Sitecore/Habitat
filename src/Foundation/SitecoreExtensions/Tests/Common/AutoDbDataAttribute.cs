namespace Sitecore.Foundation.SitecoreExtensions.Tests.Common
{
  using System;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Kernel;
  using Ploeh.AutoFixture.Xunit2;
  using Data;
  using Sitecore.FakeDb.AutoFixture;
  using System.Collections.Generic;
  using System.Reflection;
  using Data.Items;
  using Sitecore.FakeDb.Data.Items;
  using Sitecore.Globalization;
  using Xunit;

  public class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
      : base()
    {
      Fixture.Customize(new AutoDbCustomization());
      Fixture.Customize(new AutoNSubstituteCustomization());
      Fixture.Customizations.Add(new Postprocessor(new ContentAttributeRelay(),new AddContentDbItemsCommand()));
    }
  }
}