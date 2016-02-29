using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.LocalDatasource
{
  using Sitecore.Data;

  public class Templates
  {
    public struct RenderingOptions
    {
      public static ID ID = new ID("{D1592226-3898-4CE2-B190-090FD5F84A4C}");
      public struct Fields
      {
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public static readonly ID SupportsLocalDatasource = new ID("{1C307764-806C-42F0-B7CE-FC173AC8372B}");
      }
    }
  }
}