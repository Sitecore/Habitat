namespace Sitecore.Foundation.ReCaptcha.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Sitecore.Foundation.ReCaptcha.Configuration;
    using Sitecore.Foundation.ReCaptcha.Models;

    public class SiteverifyService : ISiteverifyService
    {
        private static HttpClient _httpClient;

        public HttpClient HttpClient
        {
            get
            {
                if (_httpClient != null)
                {
                    return _httpClient;
                }

                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://www.google.com")
                };

                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return _httpClient;
            }
        }

        public async Task<ReCaptchaResponse> SiteVerifyAsync(string response, bool invisible = false)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", invisible ? Settings.Invisible.Secret : Settings.V2.Secret),
                new KeyValuePair<string, string>("response", response)
            });

            HttpResponseMessage responseMessage = await this
                .HttpClient
                .PostAsync("/recaptcha/api/siteverify", content)
                .ConfigureAwait(false);

            string responseAsString = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ReCaptchaResponse>(responseAsString);
        }

    }
}