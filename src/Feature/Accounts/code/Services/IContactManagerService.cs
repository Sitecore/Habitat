using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.Accounts.Services
{
    public interface IContactManagerService
    {
        void SaveContact();
        void ReloadContact();
    }
}
