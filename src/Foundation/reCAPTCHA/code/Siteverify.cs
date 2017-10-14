namespace Sitecore.Foundation.reCAPTCHA
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using Newtonsoft.Json;
    using Sitecore.Foundation.reCAPTCHA.Configuration;

    public class Siteverify : ISiteverify
    {
        public reCAPTCHAResponse Verify(string response)
        {
            var requestUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={Settings.Secret}&response={response}";
            WebRequest webRequest = WebRequest.Create(requestUrl);

            WebResponse webResponse = webRequest.GetResponse();

            using (var reader = new StreamReader(webResponse.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.ASCII))
            {
                return JsonConvert.DeserializeObject<reCAPTCHAResponse>(reader.ReadToEnd());
            }
        }
    }
}