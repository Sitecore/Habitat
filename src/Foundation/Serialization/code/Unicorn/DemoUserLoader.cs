using IUserDataStore = Unicorn.Users.Data.IUserDataStore;
using IUserLoaderLogger = Unicorn.Users.Loader.IUserLoaderLogger;
using IUserPredicate = Unicorn.Users.UserPredicates.IUserPredicate;
using IUserSyncConfiguration = Unicorn.Users.Loader.IUserSyncConfiguration;
using SyncUserFile = Unicorn.Users.Data.SyncUserFile;
using UserLoader = Unicorn.Users.Loader.UserLoader;

namespace Sitecore.Foundation.Serialization.Unicorn
{
  using Sitecore.Configuration;

  public class DemoUserLoader : UserLoader
  {
    public DemoUserLoader(IUserPredicate userPredicate, IUserDataStore userDataStore, IUserLoaderLogger logger, IUserSyncConfiguration syncConfiguration) : base(userPredicate, userDataStore, logger, syncConfiguration)
    {
    }

    protected override string CreateNewUserPassword(SyncUserFile user)
    {
      var password = Settings.GetSetting("Sitecore.Foundation.Serialization.DemoPassword");
      return !string.IsNullOrWhiteSpace(password) ? password : base.CreateNewUserPassword(user);
    }
  }
}