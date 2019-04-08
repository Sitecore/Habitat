namespace Sitecore.Feature.Demo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Sitecore.Analytics;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Foundation.DependencyInjection;

    [Service(typeof(IReferralRepository))]
    public class ReferralRepository : IReferralRepository
    {
        private readonly ICampaignRepository campaignRepository;

        public ReferralRepository(ICampaignRepository campaignRepository)
        {
            this.campaignRepository = campaignRepository;
        }

        public Referral Get()
        {
            var campaigns = this.CreateCampaigns().ToArray();

            return new Referral {
                    Campaigns = campaigns,
                    TotalNoOfCampaigns = campaigns.Length,
                    ReferringSite = this.GetReferringSite(),
                    Keywords = GetKeywords()
                };
        }

        private static string GetKeywords()
        {
            string keywords = null;
            if (Tracker.Current != null)
            {
                keywords = Tracker.Current.Interaction.Keywords;
            }
            return keywords;
        }

        private string GetReferringSite()
        {
            if (Tracker.Current == null || HttpContext.Current == null)
            {
                return null;
            }
            var referringSite = Tracker.Current.Interaction.ReferringSite;
            return referringSite != null && referringSite.Equals(HttpContext.Current.Request.Url.Host, StringComparison.InvariantCultureIgnoreCase) ? null : referringSite;
        }

        private IEnumerable<Campaign> CreateCampaigns()
        {
            var activeCampaign = this.GetActiveCampaign();
            if (activeCampaign != null)
            {
                yield return activeCampaign;
            }

            foreach (var campaign in this.GetHistoricCampaigns())
            {
                yield return campaign;
            }
        }

        private IEnumerable<Campaign> GetHistoricCampaigns()
        {
            return this.campaignRepository.GetHistoric();
        }

        private Campaign GetActiveCampaign()
        {
            return this.campaignRepository.GetCurrent();
        }
    }
}