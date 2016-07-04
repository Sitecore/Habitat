using System;
using Newtonsoft.Json;

namespace Sitecore.Feature.Accounts.Specflow.Infrastructure
{
  

  public class ContactSearchResult
  {
    [JsonProperty("contactId")]
    public Guid ContactId { get; set; }

    [JsonProperty("preferredEmailAddress")]
    public string PreferredEmailAddress { get; set; }
  }
}