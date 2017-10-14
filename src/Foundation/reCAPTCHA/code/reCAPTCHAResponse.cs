using Newtonsoft.Json;

namespace Sitecore.Foundation.reCAPTCHA
{
    public class reCAPTCHAResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("challenge_ts")]
        public string ChallengeTs { get; set; }
        [JsonProperty("hostname")]
        public string Hostname { get; set; }
        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}