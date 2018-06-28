namespace Sitecore.Feature.Accounts.Services
{
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Client.Configuration;

    [Service(typeof(IXdbContextFactory))]
    public class XdbContextFactory : IXdbContextFactory
    {
        public IXdbContext CreateContext()
        {
            return SitecoreXConnectClientConfiguration.GetClient();
        }
    }
}