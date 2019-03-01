using System.Collections.Generic;

namespace Sitecore.Feature.Accounts.Repositories
{
    using Sitecore.Feature.Accounts.Models;

    public interface IFedAuthLoginButtonRepository
    {
        IEnumerable<FedAuthLoginButton> GetAll();
    }
}