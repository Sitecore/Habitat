namespace Sitecore.Feature.Accounts.Repositories
{
    using Sitecore.Security.Accounts;

    public interface IAccountRepository
  {
    /// <summary>
    ///   Method method changes thepassword for the user to a random string,
    ///   and returns that string.
    /// </summary>
    /// <param name="userName">Username that should have new password</param>
    /// <returns>New generated password</returns>
    string RestorePassword(string userName);
    void RegisterUser(string email, string password, string profileId);
    bool Exists(string userName);
    void Logout();
    User Login(string userName, string password);
  }
}