namespace Sitecore.Foundation.Installer.ReportingDbReplace
{
  using System;
  using System.Data.SqlClient;
  using System.IO;
  using Microsoft.SqlServer.Management.Common;
  using Microsoft.SqlServer.Management.Smo;
  using Sitecore.Diagnostics;

  public class DatabaseService : IDatabaseService
  {
    public void ReplaceDatabase(string connectionString, string dbReplacementPath)
    {
      var connection = new SqlConnectionStringBuilder(connectionString);
      ServerConnection serverConnection = null;
      try
      {
        serverConnection = new ServerConnection(new SqlConnection(connectionString));
        var server = new Server(serverConnection);

        var database = server.Databases[connection.InitialCatalog];
        var reportinPrimaryPath = database.PrimaryFilePath;
        
        server.KillAllProcesses(database.Name);
        database.SetOffline();

        File.Copy(dbReplacementPath, reportinPrimaryPath, true);
        foreach (LogFile logFile in database.LogFiles)
        {
          logFile.Drop();
        }

        database.SetOnline();
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