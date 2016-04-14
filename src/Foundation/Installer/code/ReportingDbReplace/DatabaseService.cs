namespace Sitecore.Foundation.Installer.ReportingDbReplace
{
  using System;
  using System.Collections.Specialized;
  using System.Data.SqlClient;
  using System.IO;
  using Microsoft.SqlServer.Management.Common;
  using Microsoft.SqlServer.Management.Smo;
  using Sitecore.Diagnostics;
  using Sitecore.Mvc.Extensions;

  public class DatabaseService : IDatabaseService
  {
    public void ReplaceDatabase(string connectionString, string dbReplacementPath)
    {
      var connection = new SqlConnectionStringBuilder(connectionString);
      ServerConnection serverConnection = null;
      Database database = null;
      try
      {
        serverConnection = new ServerConnection(new SqlConnection(connectionString));
        var server = new Server(serverConnection);

        database = server.Databases[connection.InitialCatalog];
        var reportingPrimaryPath = database.FileGroups[0].Files[0].FileName;
        
        server.KillAllProcesses(database.Name);
        server.DetachDatabase(connection.InitialCatalog, true);
        server.AttachDatabase(database.Name,new StringCollection() {dbReplacementPath}, AttachOptions.RebuildLog);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex, this);
      }
      finally
      {
        serverConnection?.Disconnect();
      }
    }
  }
}