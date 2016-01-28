namespace Sitecore.Feature.Accounts.Specflow.Infrastructure
{
  using Newtonsoft.Json;
  using Sitecore.Feature.Accounts.Specflow.Steps;

  public class Data
  {
    
    [JsonProperty("dataSet")]
    public  Dataset Dataset { get; set; }
  }
}