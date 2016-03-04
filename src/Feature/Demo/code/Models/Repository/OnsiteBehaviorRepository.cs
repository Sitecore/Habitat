namespace Sitecore.Feature.Demo.Models.Repository
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.SitecoreExtensions.Services;

  public  class OnsiteBehaviorRepository
  {
    private readonly GoalRepository goalRepository = new GoalRepository();
    private readonly OutcomeRepository outcomesRepository = new OutcomeRepository();
    private readonly ProfileRepository profileRepository;

    public OnsiteBehaviorRepository(IProfileProvider profileProvider)
    {
      profileRepository = new ProfileRepository(profileProvider);
    }

    public OnsiteBehavior Get()
    {
      return new OnsiteBehavior()
             {
               Goals = goalRepository.GetLatest().ToArray(),
               Outcomes = outcomesRepository.GetLatest().ToArray(),
               HistoricProfiles = profileRepository.GetProfiles(ProfilingTypes.Historic).ToArray(),
               ActiveProfiles = profileRepository.GetProfiles(ProfilingTypes.Active).ToArray()
      };
    }
  }
}