namespace Sitecore.Feature.Accounts.Specflow.Infrastructure
{
  using Newtonsoft.Json;

  public class SearchEntity
  {
    [JsonProperty("data")]
    public Data Data { get; set; }

  }
}