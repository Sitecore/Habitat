namespace Sitecore.Feature.Accounts.Repositories
{
    using System;
    using System.Web.Security;
    using Sitecore.Diagnostics;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Foundation.Accounts.Pipelines;
    using Sitecore.Pipelines;
    using Sitecore.Security.Accounts;
    using Sitecore.Security.Authentication;

    public class AccountRepository : IAccountRepository
    {
        private readonly PipelineService pipelineService;

        public AccountRepository(PipelineService pipelineService)
        {
            this.pipelineService = pipelineService;
        }

        public bool Exists(string userName)
        {
            var fullName = Context.Domain.GetFullName(userName);

            return User.Exists(fullName);
        }

        public User Login(string userName, string password)
        {
            var accountName = string.Empty;
            var domain = Context.Domain;
            if (domain != null)
            {
                accountName = domain.GetFullName(userName);
            }

            var result = AuthenticationManager.Login(accountName, password);
            if (!result)
            {
                return null;
            }

            var user = AuthenticationManager.GetActiveUser();
            this.pipelineService.RunLoggedIn(user);
            return user;
        }

        public void Logout()
        {
            var user = AuthenticationManager.GetActiveUser();
            AuthenticationManager.Logout();
            if (user != null)
                this.pipelineService.RunLoggedOut(user);
        }

        public string RestorePassword(string userName)
        {
            var domainName = Context.Domain.GetFullName(userName);
            var user = Membership.GetUser(domainName);
            if (user == null)
                throw new ArgumentException($"Could not reset password for user '{userName}'", nameof(userName));
            return user.ResetPassword();
        }

        public void RegisterUser(string email, string password, string profileId)
        {
            Assert.ArgumentNotNullOrEmpty(email, nameof(email));
            Assert.ArgumentNotNullOrEmpty(password, nameof(password));

            var fullName = Context.Domain.GetFullName(email);
            Assert.IsNotNullOrEmpty(fullName, "Can't retrieve full userName");

            var user = User.Create(fullName, password);
            user.Profile.Email = email;
            if (!string.IsNullOrEmpty(profileId))
            {
                user.Profile.ProfileItemId = profileId;
            }

            user.Profile.Save();
            this.pipelineService.RunRegistered(user);

            this.Login(email, password);
        }
    }
}