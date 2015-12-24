using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Foundation.MultiSite.Tests.Pipelines
{
  using FluentAssertions;
  using Sitecore.Foundation.MultiSite.Pipelines;
  using Sitecore.Foundation.MultiSite.Tests.Extensions;
  using Xunit;

  public class SiteDataSourceTests
  {
    [Theory]
    [AutoDbData]
    public void GetSourceSettingName_CorrectSettingsString_ReturnSettingName(SiteDataSource processor)
    {
      var setting = "media";
      var name = $"$site[{setting}]";
      var settingName = processor.GetSourceSettingName(name);
      settingName.Should().BeEquivalentTo(setting);
    }

    [Theory]
    [AutoDbData]
    public void GetSourceSettingName_IncorrectSettings_EmptyString(SiteDataSource processor)
    {
      var setting = "med.ia";
      var name = $"$site[{setting}]";
      var settingName = processor.GetSourceSettingName(name);
      settingName.Should().BeEquivalentTo(string.Empty);
    }
  }
}
