﻿namespace Sitecore.Foundation.SitecoreExtensions.Model
{
  using System.Collections.Generic;

  public class Link
  {
    public string Url { get; set; }
    public string Target { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string CssClass { get; set; }
    public string TargetId { get; set; }
    public IEnumerable<KeyValuePair<string, string>> QueryString { get; set; }
  }
}