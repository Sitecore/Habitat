namespace Habitat.Accounts
{
  using Sitecore.Data;

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
        public static readonly ID AfterLoginPage = new ID("{B128E2B3-3865-4F1C-A147-5F248676D3F5}");
        public static readonly ID FogotPasswordMailTemplate = new ID("{365254C4-1C1C-493A-9710-671574717898}");
      }
    }

    public struct MailTemplate
    {
      public static ID ID = new ID("{26DF8F38-7E1B-43D2-85DD-68DF05FA276B}");

      public struct Fields
      {
        public static readonly ID From = new ID("{8605948C-60FB-46B8-8AAA-4C52561B53BC}");
        public static readonly ID Subject = new ID("{0F45DF05-546F-462D-97C0-BA4FB2B02564}");
        public static readonly ID Body = new ID("{1519CCAD-ED26-4F60-82CA-22079AF44D16}");
      }
    }
  }
}