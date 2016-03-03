namespace Sitecore.Feature.Demo.Models.Factory
{
  using System.Collections.Generic;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.SitecoreExtensions.Services;

  public  class OnsiteBehaviorFactory
  {
    private IContactProfileProvider contactProfileProvider;
    private IProfileProvider profileProvider;

    public OnsiteBehaviorFactory(IContactProfileProvider contactProfileProvider, IProfileProvider profileProvider)
    {
      this.contactProfileProvider = contactProfileProvider;
      this.profileProvider = profileProvider;
    }

    public OnsiteBehavior Create()
    {
      return new OnsiteBehavior()
             {
               Goals = CreateGoals(),
               Outcomes = CreateOutcomes(),
               Profiles = CreateProfiles()
             };
    }

    private IEnumerable<Profile> CreateProfiles()
    {
      yield break;
    }

    private IEnumerable<Outcome> CreateOutcomes()
    {
      yield break;
    }

    private IEnumerable<Goal> CreateGoals()
    {

      yield break;
    }
  }
}