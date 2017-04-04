namespace Sitecore.Foundation.Accounts.Pipelines
{
    using System;
    using Sitecore.Pipelines;
    using Sitecore.Security.Accounts;

    public class AccountsPipelineArgs : PipelineArgs
    {
        public User User
        {
            get; set;
        }

        public string UserName
        {
            get
            {
                return this.CustomData["UserName"]?.ToString();
            }
            set
            {
                this.CustomData["UserName"] = value;
            }
        }

        public Guid? ContactId
        {
            get
            {
                return (Guid)this.CustomData["ContactId"];
            }
            set
            {
                this.CustomData["ContactId"] = value;
            }
        }
    }
}