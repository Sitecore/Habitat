namespace Sitecore.Feature.Accounts.Services.FacetUpdaters
{
    using System;
    using System.Collections.Generic;
    using Sitecore.Diagnostics;
    using Sitecore.Security;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Collection.Model;

    public class AvatarFacetUpdater : IContactFacetUpdater
    {
        private readonly IWebClient webClient;

        public IList<string> FacetsToUpdate => new[] { Avatar.DefaultFacetKey };

        public AvatarFacetUpdater(IWebClient webClient)
        {
            this.webClient = webClient;
        }

        public bool SetFacets(UserProfile profile, Contact contact, IXdbContext client)
        {
            var url = profile[Accounts.Constants.UserProfile.Fields.PictureUrl];
            var mimeType = profile[Accounts.Constants.UserProfile.Fields.PictureMimeType];
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(mimeType))
            {
                return false;
            }
            try
            {
                var pictureData = this.webClient.DownloadData(url);
                var pictureMimeType = mimeType;

                var avatar = contact.GetFacet<Avatar>(Avatar.DefaultFacetKey);
                if (avatar == null)
                {
                    avatar = new Avatar(pictureMimeType, pictureData);
                }
                else if (avatar.Picture == pictureData && avatar.MimeType == mimeType)
                {
                    return false;
                }
                else
                {
                    avatar.MimeType = pictureMimeType;
                    avatar.Picture = pictureData;
                }
                client.SetFacet(contact, Avatar.DefaultFacetKey, avatar);
                return true;
            }
            catch (Exception exception)
            {
                Log.Warn($"Could not download profile picture {url}", exception, this);
                return false;
            }
        }
    }
}