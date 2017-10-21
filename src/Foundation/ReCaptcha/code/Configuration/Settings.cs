namespace Sitecore.Foundation.ReCaptcha.Configuration
{
    public static class Settings
    {
        public struct V2
        {
            public static string SiteKey => Sitecore.Configuration.Settings.GetSetting("Foundation.ReCaptcha.Sitekey");
            public static string Secret => Sitecore.Configuration.Settings.GetSetting("Foundation.ReCaptcha.Secret");
        }

        public struct Invisible
        {
            public static string SiteKey => Sitecore.Configuration.Settings.GetSetting("Foundation.Invisible.ReCaptcha.Sitekey");
            public static string Secret => Sitecore.Configuration.Settings.GetSetting("Foundation.Invisible.ReCaptcha.Secret");
        }
        
    }
}