namespace Sitecore.Feature.Identity
{
  using Sitecore.Data;

  public struct Templates
  {
    public struct Identity
    {
      public static readonly ID ID = new ID("{FA8DE5B9-D5D8-40A7-866A-23AF4F5A9629}");

      public struct Fields
      {
        public static readonly ID Logo = new ID("{E748D808-64C1-4DEC-9718-F35CF9689E4B}");
        public static readonly ID Copyright = new ID("{02115632-FE1E-4B3D-9AD4-A4DDF1F782F0}");
        public static readonly ID OrganisationName = new ID("{EFD4980A-030C-497C-8880-40B6030AC28B}");
        public static readonly ID OrganisationAddress = new ID("{A24DF48F-C8A3-4163-966C-8C24BD8760B2}");
        public static readonly ID OrganisationPhone = new ID("{005ED83C-2D2F-4D07-A7A9-EB64D873DE46}");
        public static readonly ID OrganisationEmail = new ID("{9C428556-5D7B-46AC-B0BB-B06A4F4C9591}");
      }
    }
  }
}