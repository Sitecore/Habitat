namespace Sitecore.Foundation.Installer
{
    using System.Collections.Specialized;
    using System.IO;
    using System.Web.Security;
    using Sitecore.Diagnostics;
    using Sitecore.Install;
    using Sitecore.Install.Files;
    using Sitecore.SecurityModel;

    public class AccountsEnableAction : IPostStepAction
    {
        public void Run(NameValueCollection collection)
        {
            this.InstallSecurity(collection);

            using (new SecurityDisabler())
            {
                foreach (MembershipUser user in Membership.GetAllUsers())
                {
                    if (user.IsApproved)
                    {
                        continue;
                    }
                    Log.Info($"Enabling user {user.UserName}", this);

                    user.IsApproved = true;
                    Membership.UpdateUser(user);
                }
            }
        }

        protected void InstallSecurity(NameValueCollection metaData)
        {
            if (metaData != null)
            {
                var packageName = $"{metaData["PackageName"]}.zip";
                var installer = new Installer();
                var file = Installer.GetFilename(packageName);
                if (File.Exists(file))
                {
                    installer.InstallSecurity(PathUtils.MapPath(file));
                }
            }
        }
    }
}