namespace Sitecore.Feature.Demo.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Feature.Demo.Models;

  public class ReferralRepository
  {
    private readonly ICampaignRepository campaignRepository;

    public ReferralRepository(ICampaignRepository campaignRepository)
    {
      this.campaignRepository = campaignRepository;
    }

    public ReferralRepository() : this(new CampaignRepository())
    {
    }

    public Referral Get()
    {
      var campaigns = this.CreateCampaigns().ToArray();
      return new Referral
             {
               Campaigns = campaigns,
               TotalNoOfCampaigns = campaigns.Length,
               ReferringSite = this.GetReferringSite()
             };
    }

    private string GetReferringSite()
    {
      return Tracker.Current.Interaction.ReferringSite;
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