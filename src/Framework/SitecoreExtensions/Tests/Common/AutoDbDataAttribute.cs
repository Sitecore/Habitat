using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Sitecore.FakeDb.AutoFixture;

namespace Habitat.Framework.SitecoreExtensions.Tests.Common
{
  internal class AutoDbDataAttribute : AutoDataAttribute
  {
    public AutoDbDataAttribute()
      : base(new Fixture().Customize(new AutoDbCustomization()))
    {
    }
  }
}
