namespace Sitecore.Feature.Accounts
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
        public static readonly ID RegisterOutcome = new ID("{835FA523-D28A-46A2-A589-6AA4A5BF0846}");
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

    public struct Interest
    {
       public static ID ID = new ID("{C9B1855E-CA80-4414-B5BA-956CB67DC5A9}");

      public struct Fields
      {
        public static readonly ID Title = new ID("{1FBE5200-2C62-4A32-BA84-CFFE3CF665D3}");
      }
    }

    public struct ProfileSettigs
    {
      public static ID ID = new ID("{2D9AA1E4-3359-4F02-9EAA-5CF972FD990D}");

      public struct Fields
      {
        public static readonly ID UserProfile = new ID("{378B7D87-5775-4EB6-86B7-282D5359B1C6}");
        public static readonly ID InterestsFolder = new ID("{021AA3F7-206F-4ACC-9538-F6D7FE86B168}");
      }
    }

    public struct UserProfile
    {
      public static ID ID = new ID("{696D276B-786A-4D1E-B8BB-8E139DB7BE7C}");

      public struct Fields
      {
        public static readonly ID FirstName = new ID("{E7BC8A3E-3201-4556-B2FF-FF4DB04DB081}");
        public static readonly ID LastName = new ID("{EE21278F-4F83-4A10-8890-66B957F3D312}");
        public static readonly ID PhoneNumber = new ID("{F7A1605F-7BBB-4BC7-BBB4-9E0546648E1D}");
        public static readonly ID Interest = new ID("{A5D0B0AD-CE4E-4E06-B821-F30416B7DEC9}");
      }
    }
  }
}