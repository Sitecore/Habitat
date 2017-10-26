namespace Sitecore.Feature.Accounts.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Abstractions;
    using Sitecore.Data;
    using Sitecore.Feature.Accounts.Models;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Pipelines.GetSignInUrlInfo;

    [Service(typeof(IFedAuthLoginButtonRepository))]
    public class FedAuthLoginButtonRepository : IFedAuthLoginButtonRepository
    {
        public FedAuthLoginButtonRepository(BaseCorePipelineManager pipelineManager, IAccountsSettingsService accountsSettingsService)
        {
            this.PipelineManager = pipelineManager;
            this.AccountsSettingsService = accountsSettingsService;
        }

        public BaseCorePipelineManager PipelineManager { get; }
        public IAccountsSettingsService AccountsSettingsService { get; }

        public IEnumerable<FedAuthLoginButton> GetAll()
        {
            var returnUrl = this.AccountsSettingsService.GetPageLinkOrDefault(Context.Item, Templates.AccountsSettings.Fields.AfterLoginPage);
            var args = new GetSignInUrlInfoArgs(Context.Site.Name, returnUrl);
            GetSignInUrlInfoPipeline.Run(this.PipelineManager, args);
            if (args.Result == null)
            {
                throw new InvalidOperationException("Could not retrieve federated authentication logins");
            }
            return args.Result.Select(CreateFedAuthLoginButton).ToArray();
        }

        private static FedAuthLoginButton CreateFedAuthLoginButton(SignInUrlInfo signInInfo)
        {
            var caption = DictionaryPhraseRepository.Current.Get($"/Accounts/Sign in providers/{signInInfo.IdentityProvider}", $"Sign in with {signInInfo.Caption}");
            string iconClass = null;
            switch (signInInfo.IdentityProvider.ToLower())
            {
                case "facebook":
                    iconClass = "fa fa-facebook";
                    break;
                case "google":
                    iconClass = "fa fa-google";
                    break;
                case "linkedin":
                    iconClass = "fa fa-linkedin";
                    break;
                case "twitter":
                    iconClass = "fa fa-twitter";
                    break;
                default:
                    iconClass = "fa fa-cloud";
                    break;
            }

            return new FedAuthLoginButton
            {
                Provider = signInInfo.IdentityProvider,
                IconClass = iconClass,
                Href = signInInfo.Href,
                Caption = caption,
            };
        }
    }
}