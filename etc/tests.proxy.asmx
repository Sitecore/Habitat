<%@ WebService Language="C#" Class="AutoTestsHelperService" Debug="true" %>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Web.Security;
using System.Web.Services;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Tracking;
using Sitecore.Configuration;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Maintenance;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Exceptions;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Jobs;
using Sitecore.Resources.Media;
using Sitecore.Security;
using Sitecore.Security.AccessControl;
using Sitecore.Security.Accounts;
using Sitecore.SecurityModel;
using Sitecore.Update;
using Sitecore.Update.Installer.Utils;
using Sitecore.Update.Utils;

/// <summary>
///   Provides facilities to install the SAC solution.
/// </summary>
[WebService(Namespace = "http://sitecore.net/AutoTestsHelperService.asmx")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class AutoTestsHelperService : WebService
{
  private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["core"].ConnectionString;

  #region Public methods

  /// <summary>
  ///   Creates the user.
  /// </summary>
  /// </summary>
  /// <param name="userName">
  ///   The name of the user.
  /// </param>
  /// <param name="password">
  ///   The password of the user.
  /// </param>
  /// <returns>
  ///   <c>true</c> if the user has been created; otherwise, <c>false</c>.
  /// </returns>
  [WebMethod]
  public bool CreateUser(string userName, string password)
  {
    this.DeleteUserByUserName(userName);

    using (new SecurityDisabler())
    {
      // Creating the user.
      var user = Sitecore.Security.Accounts.User.Create(userName, password);
      // Set admin access rights
      user.Profile.IsAdministrator = true;
      user.Profile.Save();
    }

    // Check whether the user has been created.
    return Sitecore.Security.Accounts.User.Exists(userName);
  }


  /// <summary>
  ///   Set role for the user.
  /// </summary>
  /// </summary>
  /// <param name="userName">
  ///   The name of the user.
  /// </param>
  /// <param name="roleName">
  ///   The role of the user.
  /// </param>
  /// <returns>
  ///   <c>true</c> if the role has been assigned; otherwise, <c>false</c>.
  /// </returns>
  [WebMethod]
  public bool SetRoleForUser(string userName, string roleName)
  {
    var roles = UserRoles.FromUser(Sitecore.Security.Accounts.User.FromName(userName, true));
    var role = Role.FromName(roleName);
    roles.Add(role);
    return roles.Contains(role);
  }

  /// <summary>
  ///   Gets a list of the roles that a user is in.
  /// </summary>
  /// <returns>
  ///   A string array containing the names of all the roles that the specified user is in.
  /// </returns>
  /// <param name="username">The user to return a list of roles for. </param>
  /// <exception cref="T:System.ArgumentNullException"><paramref name="username" /> is null. </exception>
  /// <exception cref="T:System.ArgumentException"><paramref name="username" /> contains a comma (,). </exception>
  /// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
  [WebMethod]
  public string[] GetRolesForUser(string userName)
  {
    return Roles.GetRolesForUser(userName);
  }


  /// <summary>
  ///   Gets a list of all the roles for the application.
  /// </summary>
  /// <returns>
  ///   A string array containing the names of all the roles stored in the data source for the application.
  /// </returns>
  /// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
  [WebMethod]
  public string[] GetAllRoles()
  {
    return Roles.GetAllRoles();
  }


  [WebMethod]
  public bool GetFieldSecurityRight(string database, string itemIdOrPath, string fieldId, string userName, string right)
  {
    var user = Sitecore.Security.Accounts.User.FromName(userName, true);

    using (new UserSwitcher(user))
    {

      var item = Database.GetDatabase(database).GetItem(itemIdOrPath);

      var field = ID.IsID(fieldId) ? item.Fields[ID.Parse(fieldId)] : item.Fields[fieldId];
      if (field == null)
      {
        throw new Exception("Not supported field: " + fieldId);
      }
      return right == "Write" ? field.CanWrite : field.CanRead;

    }
  }


  [WebMethod]
  public bool GetItemSecurityRight(string database, string itemIdOrPath, string userName, string right)
  {
    Assert.ArgumentNotNull(database, nameof(database));
    Assert.ArgumentNotNull(itemIdOrPath, nameof(itemIdOrPath));
    Assert.ArgumentNotNull(userName, nameof(userName));
    Assert.ArgumentNotNull(right, nameof(right));
    var user = Sitecore.Security.Accounts.User.FromName(userName, true);

    using (new SecurityDisabler())
    {
      var item = Database.GetDatabase(database).GetItem(itemIdOrPath);

      if (item == null)
      {
        throw new ItemNotFoundException(itemIdOrPath);
      }
      var propertyInfo = typeof(AccessRight).GetProperty(right, BindingFlags.Static | BindingFlags.Public);
      var accessRight = propertyInfo.GetValue(null) as AccessRight;

      return AuthorizationManager.IsAllowed(item, accessRight, user);
    }

  }

  [WebMethod]
  public List<SerializableKeyValuePair<string, string>> GetUserProperties(string userName)
  {
    var user = Sitecore.Security.Accounts.User.FromName(userName, true);
    return user.Profile.GetCustomPropertyNames().Select(x => new SerializableKeyValuePair<string, string>(x, user.Profile.GetCustomProperty(x))).ToList();
  }

  [WebMethod]
  public List<string> GetContactTags(string identifier, string tagName)
  {
    Assert.ArgumentNotNullOrEmpty(identifier, "identifier");
    Assert.ArgumentNotNullOrEmpty(tagName, "tagName");

    var contactManager = (ContactManager)Factory.CreateObject("tracking/contactManager", false);
    var contact = contactManager.LoadContactReadOnly(identifier);
    return contact.Tags.GetAll(tagName).ToList();
  }

  [WebMethod]
  public SystemInfo GetContactSystemInfo(string identifier)
  {
    Assert.ArgumentNotNullOrEmpty(identifier, "identifier");

    var contactManager = (ContactManager)Factory.CreateObject("tracking/contactManager", false);
    var contact = contactManager.LoadContactReadOnly(identifier);

    return new SystemInfo
    {
      Value = contact.System.Value,
      VisitCount = contact.System.VisitCount
    };
  }

  [WebMethod]
  public PersonalInfo GetContactPersonalInfo(string identifier)
  {
    Assert.ArgumentNotNullOrEmpty(identifier, "identifier");

    var contactManager = (ContactManager)Factory.CreateObject("tracking/contactManager", false);
    var contact = contactManager.LoadContactReadOnly(identifier);
    var personalInfo = contact.GetFacet<IContactPersonalInfo>("Personal");
    return new PersonalInfo
    {
      FirstName = personalInfo.FirstName,
      Surname = personalInfo.Surname
    };
  }

  [WebMethod]
  public string GetContactEmail(string identifier, string emailKey = "Primary")
  {
    Assert.ArgumentNotNullOrEmpty(identifier, "identifier");

    var contactManager = (ContactManager)Factory.CreateObject("tracking/contactManager", false);
    var contact = contactManager.LoadContactReadOnly(identifier);
    var emails = contact.GetFacet<IContactEmailAddresses>("Emails");
    return emails.Entries[emailKey].SmtpAddress;
  }

  [WebMethod]
  public string GetContactPhone(string identifier, string phoneKey = "Primary")
  {
    Assert.ArgumentNotNullOrEmpty(identifier, "identifier");

    var contactManager = (ContactManager)Factory.CreateObject("tracking/contactManager", false);
    var contact = contactManager.LoadContactReadOnly(identifier);
    var phones = contact.GetFacet<IContactPhoneNumbers>("Phone Numbers");
    return phones.Entries[phoneKey].Number;
  }

  [Serializable]
  public class PersonalInfo
  {
    public string FirstName { get; set; }
    public string Surname { get; set; }
  }

  [Serializable]
  public class SystemInfo
  {
    public int VisitCount { get; set; }
    public int Value { get; set; }
  }

  [Serializable]
  public class SerializableKeyValuePair<TKey, TValue>
  {
    public SerializableKeyValuePair()
    {
    }

    public SerializableKeyValuePair(TKey key, TValue value)
    {
      this.Key = key;
      this.Value = value;
    }

    public TKey Key { get; set; }
    public TValue Value { get; set; }
  }


  /// <summary>
  ///   Deletes the user.
  /// </summary>
  /// <param name="userName">
  ///   The name of the user.
  /// </param>
  /// <returns>
  ///   <c>true</c> if the user has been deleted; otherwise, <c>false</c>.
  /// </returns>
  [WebMethod]
  public bool DeleteUser(string userName)
  {
    this.DeleteUserByUserName(userName);

    // Check whether the user has been deleted.
    return !Sitecore.Security.Accounts.User.Exists(userName);
  }

  [WebMethod(Description = "Indexes core and master databases")]
  public void IndexDatabases()
  {
    IndexDatabase("core");
    IndexDatabase("master");
  }

  [WebMethod(Description = "Indexes master database")]
  public void IndexMasterDatabases()
  {
    try
    {
      using (new SecurityDisabler())
      {
        var idx = ContentSearchManager.GetIndex("sitecore_master_index");
        var job = IndexCustodian.FullRebuild(idx, true);

        while (true)
        {
          if (job.IsDone)
          {
            break;
          }

          Thread.Sleep(500);
        }
      }
    }
    catch
    {
    }
  }

  [WebMethod(Description = "Create new media item")]
  public string UploadMediaItem(string itemPath, string fileName, byte[] fileContent)
  {
    using (new SecurityDisabler())
    {
      using (var stream = new MemoryStream(fileContent))
      {
        var options = new MediaCreatorOptions
        {
          Destination = itemPath,
          Database = Database.GetDatabase("master"),
          IncludeExtensionInItemName = false
        };

        var creator = new MediaCreator();
        var item = creator.CreateFromStream(stream, fileName, options);

        var mediaItem = (MediaItem)item;
        return MediaManager.GetMediaUrl(item);
      }
    }
  }

 [WebMethod(Description = "Get Template id of item")]
 public string GetTemplateId(string itemIdOrPath) 
 {
    using (new SecurityDisabler())
    {  
      var item = Database.GetDatabase("master").GetItem(itemIdOrPath);
      return item.TemplateID.ToString();
    }
 }

[WebMethod(Description = "Add version of item")]
public bool AddItemVersion(string itemIdOrPath, string language) 
{
    using (new SecurityDisabler())
    {  
      var item = Database.GetDatabase("master").GetItem(itemIdOrPath,Sitecore.Globalization.Language.Parse(language));
      return item.Versions.AddVersion()!=null; 
    }
}


  [WebMethod(Description = "Attach file content to media item")]
  public string AttachToMediaItem(string itemPath, string fileName, byte[] fileContent)
  {
    using (new SecurityDisabler())
    {
      using (var stream = new MemoryStream(fileContent))
      {
        var options = new MediaCreatorOptions
        {
          Database = Database.GetDatabase("master"),
          IncludeExtensionInItemName = false
        };

        var creator = new MediaCreator();

        var item = creator.AttachStreamToMediaItem(stream, itemPath, fileName, options);

        var mediaItem = (MediaItem)item;
        return MediaManager.GetMediaUrl(item);
      }
    }
  }

  [WebMethod(Description = "Rename item")]
  public string RenameItem(string itemPath, string newName)
  {
    using (new SecurityDisabler())
    {
      var item = Database.GetDatabase("master").GetItem(itemPath);
      item.Editing.BeginEdit();
      item.Name = newName;
      item.Editing.EndEdit();

      return item.Name;
    }
  }

  [WebMethod(Description = "Configure host name for habitat site item")]
  public void SetSiteHostName(string siteName, string hostName)
  {
    using (new SecurityDisabler())
    {
      var content = Sitecore.Context.Database.GetItem("/sitecore/content");
      var siteChildern = GetChildrenDerivedFrom(content,new ID("{BB85C5C2-9F87-48CE-8012-AF67CF4F765D}"));
      var site = siteChildern.FirstOrDefault(x => x.Name.Equals(siteName, StringComparison.InvariantCultureIgnoreCase));
      if (site == null)
      {
        throw new Exception($"Can't find site with name {siteName}");
      }
      site.Editing.BeginEdit();
      site.Fields[new ID("{B8027200-AAB0-4876-B454-DBB85ADD9E3C}")].Value = hostName;
      site.Editing.EndEdit();
    }
  }
 
  public static IEnumerable<Item> GetChildrenDerivedFrom(Item item, ID templateId)
  {
    return item.GetChildren().Where(c => c.IsDerived(templateId));
  }

  #endregion

  #region Private static methods

  private static void AlterPasswordDbRecord(string email, string password, string passwordSalt)
  {
    var command = new SqlCommand("UPDATE [aspnet_Membership] SET [Password] = '" + password + "', [PasswordSalt] = '" + passwordSalt + "' WHERE [Email] = '" + email + "'");

    ExecuteNonQuery(command);
  }

  private static int ExecuteNonQuery(SqlCommand command)
  {
    using (var connection = new SqlConnection(ConnStr))
    {
      connection.Open();
      command.Connection = connection;
      return command.ExecuteNonQuery();
    }
  }

  private static void IndexDatabase(string name)
  {
    var database = Database.GetDatabase(name);
    var updateJobName = string.Format("UpdateIndex_{0}", name);

    Job job;
    if (!JobManager.IsJobRunning(updateJobName))
    {
      var options = new JobOptions(updateJobName, "Indexing", "system", new IndexingManager(), "UpdateIndex",
        new object[]
        {
          database
        })
      {
        AtomicExecution = true
      };
      job = new Job(options);
      JobManager.Start(job);
    }
    else
    {
      job = JobManager.GetJob(updateJobName);
    }

    job.Wait();
  }

  private static InstallResponse CreateSuccessInstallResponse()
  {
    return new InstallResponse
    {
      Success = true
    };
  }

  private static InstallResponse CreateErrorInstallResponse(Exception e)
  {
    return new InstallResponse
    {
      Message = GetErrorString(e),
      Success = false
    };
  }

  private static string GetErrorString(Exception e)
  {
    var error = new StringBuilder();
    GetErrorMessage(error, e);
    return error.ToString();
  }

  private static void GetErrorMessage(StringBuilder error, Exception e)
  {
    error.AppendLine(e.Message);
    error.AppendLine(e.StackTrace);

    if (e.InnerException == null)
    {
      return;
    }

    error.AppendLine(string.Empty);
    error.AppendLine("INNER EXCEPTION:");
    error.AppendLine(string.Empty);
    GetErrorMessage(error, e.InnerException);
  }

  #endregion

  #region Protected methods

  protected PackageInstallationInfo GetInstallationInfo(string packagePath)
  {
    var installationInfo = new PackageInstallationInfo
    {
      Mode = InstallMode.Install,
      Action = UpgradeAction.Upgrade,
      Path = packagePath
    };

    return installationInfo;
  }

  #endregion

  #region Private methods

  /// <summary>
  ///   Deletes the user by username.
  /// </summary>
  /// <param name="userName">
  ///   The name of the user.
  /// </param>
  private void DeleteUserByUserName(string userName)
  {
    // Check whether the specified user exists.
    var user = Sitecore.Security.Accounts.User.FromName(userName, true);

    // Deleting the existent user.
    if (user != null)
    {
      user.Delete();
    }
  }

  #endregion

  [KnownType("InstallResponse")]
  public class InstallResponse
  {
    public string Message { get; set; }
    public bool Success { get; set; }
  }
}
