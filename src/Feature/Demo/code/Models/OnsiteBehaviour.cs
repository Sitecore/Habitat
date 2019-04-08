﻿namespace Sitecore.Feature.Demo.Models
{
    using System.Collections.Generic;
    using Sitecore.XConnect;

    public class OnsiteBehavior
    {
        public IEnumerable<Profile> ActiveProfiles { get; set; }
        public IEnumerable<Profile> HistoricProfiles { get; set; }
        public IEnumerable<PageEvent> Goals { get; set; }
        public IEnumerable<Outcome> Outcomes { get; set; }
        public IEnumerable<PageEvent> PageEvents { get; set; }
    }
}