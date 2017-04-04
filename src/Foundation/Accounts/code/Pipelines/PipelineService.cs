namespace Sitecore.Foundation.Accounts.Pipelines
{
    using Sitecore.Analytics;
    using Sitecore.Pipelines;
    using Sitecore.Security.Accounts;

    public class PipelineService
    {
        public bool RunLoggedIn(User user)
        {
            
            var args = new LoggedInPipelineArgs()
            {
                User = user,
                UserName = user.Name,
                ContactId = Tracker.Current?.Contact?.ContactId
            };
            CorePipeline.Run("accounts.loggedIn", args);
            return args.Aborted;
        }

        public bool RunLoggedOut(User user)
        {
            var args = new AccountsPipelineArgs()
            {
                User = user,
                UserName = user.Name
            };
            CorePipeline.Run("accounts.loggedOut", args);
            return args.Aborted;
        }

        public bool RunRegistered(User user)
        {
            var args = new AccountsPipelineArgs()
            {
                User = user,
                UserName = user.Name
            };
            CorePipeline.Run("accounts.registered", args);
            return args.Aborted;
        }
    }
}