using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.MultiSite.Providers
{
  using System.Text.RegularExpressions;

  public static class DatasourceConfigurationService
  {
    public const string SiteDatasourcePrefix = "site:";
    public const string SiteDatasourceMatchPattern = @"^" + SiteDatasourcePrefix + @"(\w*)$";
    public static string GetSiteDatasourceConfigurationName(string datasourceLocationValue)
    {
      var match = Regex.Match(datasourceLocationValue, SiteDatasourceMatchPattern);
      return !match.Success ? null : match.Groups[1].Value;
    }

    public static bool IsSiteDatasourceLocation(string datasourceLocationValue)
    {
      var match = Regex.Match(datasourceLocationValue, SiteDatasourceMatchPattern);
      return match.Success;
    }

  }
}