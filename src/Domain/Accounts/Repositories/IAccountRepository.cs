namespace Habitat.Accounts.Repositories
{
  using Habitat.Accounts.Models;

  public interface IAccountRepository
  {
    /// <summary>
    /// Method method changes thepassword for the user to a random string, 
    /// and returns that string.
    /// </summary>
    /// <param name="userName">Username that should have new password</param>
    /// <returns>New generated password</returns>
    string RestorePassword(string userName);
    void RegisterUser(RegistrationInfo registrationInfo);
    bool Exists(string userName);
    void Logout();
    bool Login(string userName, string password);
  }
}
