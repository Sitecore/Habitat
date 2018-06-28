namespace Sitecore.Feature.Accounts.Services.FacetUpdaters
{
    using System.Collections.Generic;
    using Sitecore.Security;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Collection.Model;

    public class EmailFacetUpdater : IContactFacetUpdater
    {
        public IList<string> FacetsToUpdate => new[] { EmailAddressList.DefaultFacetKey };

        public bool SetFacets(UserProfile profile, Contact contact, IXdbContext client)
        {
            var email = profile.Email;
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            var emails = contact.GetFacet<EmailAddressList>(EmailAddressList.DefaultFacetKey);
            if (emails == null)
            {
                emails = new EmailAddressList(new EmailAddress(email, false), null);
            }
            else
            {
                if (emails.PreferredEmail?.SmtpAddress == email)
                {
                    return false;
                }
                emails.PreferredEmail = new EmailAddress(email, false);
            }
            client.SetFacet(contact, EmailAddressList.DefaultFacetKey, emails);
            return true;
        }
    }
}