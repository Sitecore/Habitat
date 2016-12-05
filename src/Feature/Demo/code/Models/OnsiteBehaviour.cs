namespace Sitecore.Feature.Demo.Models
{
    using System.Collections.Generic;

    public class OnsiteBehavior
    {
        public IEnumerable<Profile> ActiveProfiles { get; set; }
        public IEnumerable<Profile> HistoricProfiles { get; set; }
        public IEnumerable<PageEvent> Goals { get; set; }
        public IEnumerable<Outcome> Outcomes { get; set; }
        public PageEvent[] PageEvents { get; set; }
    }
}