using FacebookAuthenticationExtensions = Owin.FacebookAuthenticationExtensions;

namespace Sitecore.Feature.Accounts.Infrastructure.Pipelines.IdentityProviders
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.Owin.Infrastructure;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Facebook;
    using Sitecore.Abstractions;
    using Sitecore.Diagnostics;
    using Sitecore.Owin.Authentication.Configuration;
    using Sitecore.Owin.Authentication.Extensions;
    using Sitecore.Owin.Authentication.Pipelines.IdentityProviders;
    using Sitecore.Owin.Authentication.Services;

    public class Facebook : IdentityProvidersProcessor
    {
        public Facebook(FederatedAuthenticationConfiguration federatedAuthenticationConfiguration,
                        ICookieManager cookieManager,
                        BaseSettings settings)
                        : base(federatedAuthenticationConfiguration, cookieManager, settings)
        {
        }

        protected override string IdentityProviderName => "Facebook";

        protected override void ProcessCore([NotNull] IdentityProvidersArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            var identityProvider = this.GetIdentityProvider();
            var authenticationType = this.GetAuthenticationType();

            var options = new FacebookAuthenticationOptions
            {
                Caption = identityProvider.Caption,
                AuthenticationType = authenticationType,
                AuthenticationMode = AuthenticationMode.Passive,
                AppId = Settings.GetSetting("Sitecore.Feature.Accounts.Facebook.AppId"),
                AppSecret = Settings.GetSetting("Sitecore.Feature.Accounts.Facebook.AppSecret"),
                Provider = new FacebookAuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.ApplyClaimsTransformations(new TransformationContext(this.FederatedAuthenticationConfiguration, identityProvider));
                        AddClaim(context, "first_name", "first_name");
                        AddClaim(context, "middle_name", "middle_name");
                        AddClaim(context, "last_name", "last_name");
                        AddClaim(context, "full_name", "name");
                        AddClaim(context, "gender", "gender");
                        AddClaim(context, "birthday", "birthday");
                        AddClaim(context, "link", "link");
                        AddClaim(context, "locale", "locale");
                        AddClaim(context, "location", "location");
                        AddClaim(context, "picture", "picture");
                        AddClaim(context, "timezone", "timezone");
                        return Task.CompletedTask;
                    }
                },
                Scope =
                {
                    "public_profile",
                    "email"
                },
                Fields =
                {
                    "first_name",
                    "middle_name",
                    "last_name",
                    "name",
                    "email",
                    "age_range",
                    "link",
                    "gender",
                    "locale",
                    "location",
                    "picture",
                    "timezone",
                    "updated_time",
                    "verified"
                }
            };

            FacebookAuthenticationExtensions.UseFacebookAuthentication(args.App, options);
        }

        private void AddClaim(FacebookAuthenticatedContext context, string claimName, string propertyName)
        {
            var value = context.User[propertyName]?.ToString();
            if (propertyName == "picture")
            {
                value = context.User["picture"]?["data"]?["url"]?.ToString();
                if (value == null)
                    return;
                context.Identity.AddClaim(new Claim("picture_url", value));
                context.Identity.AddClaim(new Claim("picture_mime", "image/jpg"));
                return;
            }

            if (value == null)
                return;
            context.Identity.AddClaim(new Claim(claimName, value));
        }

    }

}