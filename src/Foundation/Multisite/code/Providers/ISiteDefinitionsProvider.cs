﻿namespace Sitecore.Foundation.MultiSite.Providers
{
  using System.Collections.Generic;
  using Sitecore.Web;

  public interface ISiteDefinitionsProvider
  {
    IEnumerable<SiteDefinitionItem> SiteDefinitions { get; }
  }
}
