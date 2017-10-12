using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.Assets.Compression
{
    using ClientDependency.Core.Logging;
    using Sitecore.Diagnostics;

    public class ClientDependencyLogger : ILogger
    {
        private HttpContextBase GetHttpContext()
        {
            if (HttpContext.Current == null)
                return null;
            return new HttpContextWrapper(HttpContext.Current);
        }

        private void Trace(string msg, bool isWarn = false, string category = "ClientDependency")
        {
            var http = GetHttpContext();
            if (http == null) return;
            if (isWarn)
            {
                Log.Warn(msg, this);
            }
            else
            {
                Log.Debug(msg, this);
            }
        }

        public void Debug(string msg)
        {
            Log.Debug(msg, this);
        }

        public void Info(string msg)
        {
            Log.Info(msg, this);
        }

        public void Warn(string msg)
        {
            Log.Warn(msg, this);
        }

        public void Error(string msg, Exception ex)
        {
            Log.Error(msg, this);
        }

        public void Fatal(string msg, Exception ex)
        {
            Log.Fatal(msg, this);
        }
    }
}