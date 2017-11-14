namespace Sitecore.Foundation.ReCaptcha.Configuration
{
    public static class Settings
    {
        public struct V2
        {
            public static string SiteKey => Sitecore.Configuration.Settings.GetSetting("Foundation.ReCaptcha.V2.SiteKey");
            public static string Secret => Sitecore.Configuration.Settings.GetSetting("Foundation.ReCaptcha.V2.Secret");
        }

        public struct Invisible
        {
            public static string SiteKey => Sitecore.Configuration.Settings.GetSetting("Foundation.ReCaptcha.Invisible.SiteKey");
            public static string Secret => Sitecore.Configuration.Settings.GetSetting("Foundation.ReCaptcha.Invisible.Secret");
        }
        
    }
}