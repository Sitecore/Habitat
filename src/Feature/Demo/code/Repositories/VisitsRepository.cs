namespace Sitecore.Feature.Demo.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Foundation.Accounts.Providers;
    using Sitecore.Foundation.DependencyInjection;

    [Service(typeof(IVisitsRepository))]
    public class VisitsRepository : IVisitsRepository
    {
        private readonly IContactFacetsProvider contactFacetsProvider;
        private readonly IEngagementPlanStateRepository engagementPlanStateRepository;
        private readonly IPageViewRepository pageViewRepository;

        public VisitsRepository(IContactFacetsProvider contactFacetsProvider, IEngagementPlanStateRepository engagementPlanStateRepository, IPageViewRepository pageViewRepository)
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