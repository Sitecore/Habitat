namespace Sitecore.Foundation.Accounts.Pipelines
{
    using System;

    public class LoggedInPipelineArgs : AccountsPipelineArgs
    {
        public Guid? PreviousContactId
        {
            get
            {
                return (Guid)this.CustomData["PreviousContactId"];
            }
            set
            {
                this.CustomData["PreviousContactId"] = value;
            }
        }
    }
}