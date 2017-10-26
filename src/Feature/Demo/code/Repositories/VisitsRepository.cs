namespace Sitecore.Feature.Demo.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Foundation.Accounts.Providers;
    using Sitecore.Foundation.DependencyInjection;

    [Service]
    public class VisitsRepository
    {
        private readonly IContactFacetsProvider contactFacetsProvider;
        private readonly EngagementPlanStateRepository engagementPlanStateRepository;
        private readonly PageViewRepository pageViewRepository;

        public VisitsRepository(IContactFacetsProvider contactFacetsProvider, EngagementPlanStateRepository engagementPlanStateRepository, PageViewRepository pageViewRepository)
        {
            this.contactFacetsProvider = contactFacetsProvider;
            this.engagementPlanStateRepository = engagementPlanStateRepository;
            this.pageViewRepository = pageViewRepository;
        }

        public Visits Get()
        {
            var allPageViews = this.GetAllPageViews().ToArray();
            return new Visits
            {
                EngagementValue = this.GetEngagementValue(),
                PageViews = allPageViews.Take(10),
                TotalPageViews = allPageViews.Count(),
                TotalVisits = this.GetTotalVisits(),
                EngagementPlanStates = this.engagementPlanStateRepository.GetCurrent().ToArray()
            };
        }

        private int GetEngagementValue()
        {
            return this.contactFacetsProvider.Contact?.System.Value ?? 0;
        }

        private IEnumerable<PageView> GetAllPageViews()
        {
            return Tracker.Current.Interaction.GetPages().Cast<ICurrentPageContext>().Where(x => !x.IsCancelled).Select(pc => pageViewRepository.Get(pc)).Reverse();
        }

        private int GetTotalVisits()
        {
            return this.contactFacetsProvider.Contact?.System?.VisitCount ?? 1;
        }
    }
}