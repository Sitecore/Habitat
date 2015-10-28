using System;
using System.Collections;
using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Habitat.Teasers.Models
{
    public class AccordeonModel
    {
        public AccordeonModel(Item item)
        {
            Id = $"accordion-{Guid.NewGuid().ToString("N")}";
        }

        public string Id { get; private set; }

        public IEnumerable<AccordeonElement> Elements => new[] {new AccordeonElement("Test1"), new AccordeonElement("Test2"), new AccordeonElement("Test3")};
    }

    public class AccordeonElement
    {
        public AccordeonElement(string title)
        {
            Title = title;
            HeaderId = $"accordion-header-{Guid.NewGuid().ToString("N")}";
            PanelId = $"accordion-panel-{Guid.NewGuid().ToString("N")}";
        }

        public string HeaderId { get; private set; }
        public string PanelId { get; private set; }
        public string Title { get; private set; }
    }
}