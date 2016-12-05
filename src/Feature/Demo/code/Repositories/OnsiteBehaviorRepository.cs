namespace Sitecore.Feature.Demo.Repositories
{
    using System.Linq;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Feature.Demo.Services;

    public class OnsiteBehaviorRepository
    {
        private readonly PageEventRepository pageEventRepository = new PageEventRepository();
        private readonly OutcomeRepository outcomesRepository = new OutcomeRepository();
        private readonly ProfileRepository profileRepository;

        public OnsiteBehaviorRepository(IProfileProvider profileProvider)
        {
            this.profileRepository = new ProfileRepository(profileProvider);
        }

        public OnsiteBehavior Get()
        {
            return new OnsiteBehavior
                   {
                       Goals = this.pageEventRepository.GetLatest().ToArray(),
                       PageEvents = this.pageEventRepository.GetPageEvents().ToArray(),
                       Outcomes = this.outcomesRepository.GetLatest().ToArray(),
                       HistoricProfiles = this.profileRepository.GetProfiles(ProfilingTypes.Historic).ToArray(),
                       ActiveProfiles = this.profileRepository.GetProfiles(ProfilingTypes.Active).ToArray()
                   };
        }
    }
}