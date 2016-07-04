using Newtonsoft.Json;
using Sitecore.Feature.Accounts.Specflow.Steps;

namespace Sitecore.Feature.Accounts.Specflow.Infrastructure
{
  

  public class Dataset
  {
    [JsonProperty("outcome-detail")]
    public OutcomeDetail[] OutcomeDetail { get; set; }
    public ContactSearchResult[] ContactSearchResults { get; set; }
    [JsonProperty("goals")]
    public GoalSearchResult[] Goals { get; set; }
  }
}