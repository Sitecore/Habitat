namespace Sitecore.Foundation.Installer
{
    public interface IProcessRunner
    {
        void Run(string commandPath, string arguments);
    }
}