namespace Sitecore.Feature.Demo.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Feature.Demo.Models;
  using Sitecore.Foundation.SitecoreExtensions.Services;

  public class VisitsRepository
  {
    private readonly IContactProfileProvider contactProfileProvider;
    private readonly EngagementPlanStateRepository engagementPlanStateRepository = new EngagementPlanStateRepository();

    public VisitsRepository(IContactProfileProvider contactProfileProvider)
    {
      this.contactProfileProvider = contactProfileProvider;
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
      return this.contactProfileProvider.Contact.System.Value;
    }

    private IEnumerable<PageView> GetAllPageViews()
    {
      var pageViewRepository = new PageViewRepository();
      return Tracker.Current.Interaction.GetPages().Cast<ICurrentPageContext>().Where(x => !x.IsCancelled).Select(pc => pageViewRepository.Get(pc)).Reverse();
    }

    private int GetTotalVisits()
    {
      return this.contactProfileProvider.Contact.System?.VisitCount ?? 1;
    }
  }
}