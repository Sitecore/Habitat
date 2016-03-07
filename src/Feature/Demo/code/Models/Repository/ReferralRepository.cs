namespace Sitecore.Feature.Demo.Models.Repository
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;

  public class ReferralRepository
  {
    private readonly CampaignRepository campaignRepository = new CampaignRepository();

    public Referral Get()
    {
      var campaigns = CreateCampaigns().ToArray();
      return new Referral
             {
               Campaigns = campaigns,
               TotalNoOfCampaigns = campaigns.Length,
               ReferringSite = GetReferringSite()
             };
    }

    private string GetReferringSite()
    {
      return Tracker.Current.Interaction.ReferringSite;
    }

    private IEnumerable<Campaign> CreateCampaigns()
    {
      var activeCampaign = GetActiveCampaign();
      if (activeCampaign != null)
      {
        yield return activeCampaign;
      }

      foreach (var campaign in GetHistoricCampaigns())
      {
        yield return campaign;
      }
    }

    private IEnumerable<Campaign> GetHistoricCampaigns()
    {
      return campaignRepository.GetHistoric();
    }

    private Campaign GetActiveCampaign()
    {
      return campaignRepository.GetCurrent();
    }
  }
}