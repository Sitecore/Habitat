namespace Sitecore.Feature.Accounts.Specflow.Infrastructure
{
  using System;
  using Newtonsoft.Json;

  public class ContactSearchResult
  {
    [JsonProperty("contactId")]
    public Guid ContactId { get; set; }

    [JsonProperty("preferredEmailAddress")]
    public string PreferredEmailAddress { get; set; }
  }
}