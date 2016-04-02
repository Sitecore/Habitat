﻿namespace Sitecore.Feature.Accounts.Specflow.Infrastructure
{
  using Newtonsoft.Json;

  public class SearchEntity
  {
    [JsonProperty("data")]
    public Data Data { get; set; }

  }



  public class ContactEntity
  {
    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("surName")]
    public string SurName { get; set; }

    [JsonProperty("phoneNumbers")]
    public object[] PhoneNumbers { get; set; }
    


  }
}