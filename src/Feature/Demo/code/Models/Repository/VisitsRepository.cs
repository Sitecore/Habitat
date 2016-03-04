namespace Sitecore.Feature.Demo.Models.Repository
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Mvc.Extensions;

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
      var allPageViews = GetAllPageViews().ToArray();
      return new Visits
             {
               EngagementValue = GetEngagementValue(),
               PageViews = allPageViews.Take(10),
               TotalPageViews = allPageViews.Count(),
               TotalVisits = GetTotalVisits(),
               EngagementPlanStates = engagementPlanStateRepository.GetCurrent().ToArray(),
              };
    }

    private int GetEngagementValue()
    {
      return contactProfileProvider.Contact.System.Value;
    }

    private IEnumerable<PageView> GetAllPageViews()
    {
      var pageViewRepository = new PageViewRepository();
      return Tracker.Current.Interaction.GetPages().Cast<ICurrentPageContext>().Where(x => !x.IsCancelled).Select(pc => pageViewRepository.Get(pc)).Reverse();
    }

    private int GetTotalVisits()
    {
      return contactProfileProvider.Contact.System?.VisitCount ?? 1;
    }
  }
}