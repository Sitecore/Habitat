namespace Sitecore.Feature.Accounts.Services
{
    using Sitecore.XConnect;

    public interface IXdbContextFactory
    {
        IXdbContext CreateContext();
    }
}
