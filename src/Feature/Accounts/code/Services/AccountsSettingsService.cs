namespace Sitecore.Feature.Accounts.Services
{
    using System;
    using System.Net.Mail;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Exceptions;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;

    [Service(typeof(IAccountsSettingsService))]
    public class AccountsSettingsService : IAccountsSettingsService
    {
        public static readonly string PageNotFoundUrl = Settings.GetSetting("Sitecore.Feature.Accounts.PageNotFoundUrl", "/404");
        public static AccountsSettingsService Instance => new AccountsSettingsService();

        public virtual string GetPageLink(Item contextItem, ID fieldID)
        {
            var item = this.GetAccountsSettingsItem(contextItem);
            if (item == null)
            {
                throw new Exception("Page with accounts settings isn't specified");
            }

            InternalLinkField link = item.Fields[fieldID];
            if (link.TargetItem == null)
            {
                throw new Exception($"{link.InnerField.Name} link isn't set");
            }

            return link.TargetItem.Url();
        }


        public virtual string GetPageLinkOrDefault(Item contextItem, ID field, Item defaultItem = null)
        {
            try
            {
                return this.GetPageLink(contextItem, field);
            }
            catch (Exception ex)
            {
                Log.Warn(ex.Message, ex, this);
                return defaultItem?.Url() ?? PageNotFoundUrl;
            }
        }

        public virtual Guid? GetRegistrationOutcome(Item contextItem)
        {
            var item = this.GetAccountsSettingsItem(contextItem);

            if (item == null)
            {
                throw new ItemNotFoundException("Page with accounts settings isn't specified");
            }

            ReferenceField field = item.Fields[Templates.AccountsSettings.Fields.RegisterOutcome];
            return field?.TargetID?.ToGuid();
        }

        public MailMessage GetForgotPasswordMailTemplate()
        {
            var settingsItem = this.GetAccountsSettingsItem(null);
            InternalLinkField link = settingsItem.Fields[Templates.AccountsSettings.Fields.ForgotPasswordMailTemplate];
            var mailTemplateItem = link.TargetItem;

            if (mailTemplateItem == null)
            {
                throw new ItemNotFoundException($"Could not find mail template item with {link.TargetID} ID");
            }

            var fromMail = mailTemplateItem.Fields[Templates.MailTemplate.Fields.From];

            if (string.IsNullOrEmpty(fromMail.Value))
            {
                throw new InvalidValueException("'From' field in mail template should be set");
            }

            var body = mailTemplateItem.Fields[Templates.MailTemplate.Fields.Body];
            var subject = mailTemplateItem.Fields[Templates.MailTemplate.Fields.Subject];

            return new MailMessage
            {
                From = new MailAddress(fromMail.Value),
                Body = body.Value,
                Subject = subject.Value
            };
        }

        public virtual Item GetAccountsSettingsItem(Item contextItem)
        {
            Item item = null;

            if (contextItem != null)
            {
                item = contextItem.GetAncestorOrSelfOfTemplate(Templates.AccountsSettings.ID);
            }
            item = item ?? Context.Site.GetContextItem(Templates.AccountsSettings.ID);

            return item;
        }
    }
}