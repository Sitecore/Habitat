using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Demo.Models
{
  using Sitecore.Analytics.Model.Entities;

  public class KeyBehaviorCacheEntryModel
  {
    public KeyBehaviorCacheEntryModel()
    {
      ElementId = Guid.NewGuid().ToString("N");
    }
    public IReadOnlyCollection<Sitecore.Analytics.Tracking.KeyBehaviorCacheEntry> Entries { get; set; }
    public string Title { get; set; }
    public string ElementId { get; set; }
  }
}