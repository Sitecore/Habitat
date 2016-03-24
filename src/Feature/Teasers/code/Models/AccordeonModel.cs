namespace Sitecore.Feature.Teasers.Models
{
  using System;
  using System.Collections.Generic;
  using Sitecore.Data.Items;

  public class AccordeonModel
  {
    public AccordeonModel()
    {
      this.Id = $"accordion-{Guid.NewGuid().ToString("N")}";
    }

    public string Id { get; private set; }

    public IEnumerable<AccordeonElement> Elements => new[]
    {
      //TODO: Hardcoded
      new AccordeonElement("Test1"), new AccordeonElement("Test2"), new AccordeonElement("Test3")
    };
  }

  public class AccordeonElement
  {
    public AccordeonElement(string title)
    {
      this.Title = title;
      this.HeaderId = $"accordion-header-{Guid.NewGuid().ToString("N")}";
      this.PanelId = $"accordion-panel-{Guid.NewGuid().ToString("N")}";
    }

    public string HeaderId { get; private set; }
    public string PanelId { get; private set; }
    public string Title { get; private set; }
  }
}