namespace Sitecore.Feature.Accounts.Services
{
    public interface IWebClient
    {
        byte[] DownloadData(string address);
    }
}
