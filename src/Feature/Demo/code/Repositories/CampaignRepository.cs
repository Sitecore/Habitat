namespace Sitecore.Feature.Demo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Sitecore.Analytics;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Marketing.Definitions;
    using Sitecore.Marketing.Definitions.Campaigns;
    using Sitecore.Marketing.Taxonomy;
    using Sitecore.Marketing.Taxonomy.Extensions;

    [Service(typeof(ICampaignRepository))]
    public class CampaignRepository : ICampaignRepository
    {
        private readonly IChannelTaxonomyManager channelTaxonomyManager;
        private readonly IDefinitionManager<ICampaignActivityDefinition> campaignDefinitionManager;

        public CampaignRepository(ITaxonomyManagerProvider taxonomyManagerProvider, IDefinitionManager<ICampaignActivityDefinition> campaignDefinitionManager)
        {
            this.channelTaxonomyManager = taxonomyManagerProvider.GetChannelManager();
            this.campaignDefinitionManager = campaignDefinitionManager;
        }

        public Campaign GetCurrent()
        {
            if (!Tracker.Current.Interaction.CampaignId.HasValue)
            {
                return null;
            }
            var campaignId = Tracker.Current.Interaction.CampaignId.Value;
            var campaign = GetCampaignDefinition(campaignId);

            return new Campaign
            {
                Title = campaign?.Name ?? DictionaryPhraseRepository.Current.Get("/Demo/Campaigns/Unknown Campaign", "(Unknown)"),
                IsActive = true,
                Date = Tracker.Current.Interaction.StartDateTime,
                Channel = this.GetChannel(campaign)
            };
        }

        public IEnumerable<Campaign> GetHistoric()
        {
            var keyBehaviourCache = Tracker.Current.Contact.KeyBehaviorCache;
            foreach (var cachedCampaign in keyBehaviourCache.Campaigns)
            {
                var campaign = GetCampaignDefinition(cachedCampaign.Id);

                yield return new Campaign
                {
                    Title = campaign?.Name ?? DictionaryPhraseRepository.Current.Get("/Demo/Campaigns/Unknown Campaign", "(Unknown)"),
                    IsActive = false,
                    Date = cachedCampaign.DateTime,
                    Channel = this.GetChannel(campaign)
                };
            }
        }

        private string GetChannel(ICampaignActivityDefinition campaign)
        {
            if (campaign?.ChannelUri == null)
            {
                return null;
            }
            var channel = channelTaxonomyManager.GetChannel(campaign.ChannelUri, Context.Language.CultureInfo);
            return channel == null ? null : channelTaxonomyManager.GetFullName(channel.Uri, "/");
        }

        private ICampaignActivityDefinition GetCampaignDefinition(Guid campaignId)
        {
            var campaign = campaignDefinitionManager.Get(campaignId, Context.Language.CultureInfo) ?? campaignDefinitionManager.Get(campaignId, CultureInfo.InvariantCulture);
            return campaign;
        }
    }
}