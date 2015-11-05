using Sitecore.Data;

namespace Habitat.Accounts
{
  public class Templates
  {
    public struct AccountsSettings
    {
      public static ID ID = new ID("{59D216D1-035C-4497-97B4-E3C5E9F1C06B}");

      public struct Fields
      {
        public static readonly ID AccountsDetailsPage = new ID("{ED71D374-8C33-4561-991D-77482AE01330}");
        public static readonly ID RegisterPage = new ID("{71962360-10D8-4B98-BB8D-57660CE11127}");
        public static readonly ID LoginPage = new ID("{60745023-FFD5-400E-8F80-4BCA9F2ABB29}");
        public static readonly ID ForgotPasswordPage = new ID("{F3CD2BB8-472B-4DF0-87C0-A13098E391CA}");
      }
    }
  }
}
