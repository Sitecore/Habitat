namespace Sitecore.Feature.Accounts.Services
{
    using Sitecore.Foundation.DependencyInjection;

    [Service(typeof(IWebClient))]
    public class WebClient : IWebClient
    {
        public byte[] DownloadData(string address)
        {
            var realWebClient = new System.Net.WebClient();
            return realWebClient.DownloadData(address);
        }
    }
}