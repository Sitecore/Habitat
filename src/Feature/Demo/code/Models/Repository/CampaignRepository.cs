namespace Sitecore.Feature.Demo.Models.Repository
{
  using System.Collections.Generic;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Common;
  using Sitecore.Data;
  using Sitecore.Foundation.Dictionary.Repositories;
  using Sitecore.Marketing.Definitions;
  using Sitecore.Marketing.Definitions.Campaigns;
  using Sitecore.Marketing.Taxonomy;
  using Sitecore.Marketing.Taxonomy.Extensions;

  internal class CampaignRepository : ICampaignRepository
  {
    public Campaign GetCurrent()
    {
      if (!Tracker.Current.Interaction.CampaignId.HasValue)
      {
        return null;
      }
      var campaignId = Tracker.Current.Interaction.CampaignId.Value.ToID();
      var campaign = GetCampaignDefinition(campaignId);

      return new Campaign
             {
               Title = campaign?.Name ?? DictionaryPhraseRepository.Current.Get("/Demo/Campaigns/Unknown Campaign", "(Unknown)"),
               IsActive = true,
               Date = Tracker.Current.Interaction.StartDateTime,
               Channel = GetChannel(campaign)
             };
    }

    private string GetChannel(ICampaignActivityDefinition campaign)
    {
      if (campaign?.ChannelUri == null)
      {
        return null;
      }
      var channelTaxonomyManager = TaxonomyManager.Provider.GetChannelManager();
      var channel = channelTaxonomyManager.GetChannel(campaign.ChannelUri, Context.Language.CultureInfo);
      return channel == null ? null : channelTaxonomyManager.GetFullName(channel.Uri, "/");
    }

    private static ICampaignActivityDefinition GetCampaignDefinition(ID campaignId)
    {
      var campaigns = DefinitionManagerFactory.Default.GetDefinitionManager<ICampaignActivityDefinition>();
      var campaign = campaigns.Get(campaignId, Context.Language.CultureInfo);
      return campaign;
    }

    public IEnumerable<Campaign> GetHistoric()
    {
      Tracker.Current.Contact.LoadKeyBehaviorCache();
      var keyBehaviourCache = Tracker.Current.Contact.GetKeyBehaviorCache();
      foreach (var cachedCampaign in keyBehaviourCache.Campaigns)
      {
        var campaign = GetCampaignDefinition(cachedCampaign.Id.ToID());

        yield return new Campaign
                     {
                       Title = campaign?.Name ?? DictionaryPhraseRepository.Current.Get("/Demo/Campaigns/Unknown Campaign", "(Unknown)"),
                       IsActive = false,
                       Date = cachedCampaign.DateTime,
                       Channel = GetChannel(campaign)
                     };
      }
    }
  }
}