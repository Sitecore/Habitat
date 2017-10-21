namespace Sitecore.Foundation.ReCaptcha.Configuration
{
    public static class Settings
    {
        public struct V2
        {
            public static string SiteKey => Sitecore.Configuration.Settings.GetSetting("Foundation.ReCaptcha.Sitekey", "6LeYNx0TAAAAAM5o86DlvSMR4xSCyu5pz7O0jujG");
            public static string Secret => Sitecore.Configuration.Settings.GetSetting("Foundation.ReCaptcha.Secret", "6LeYNx0TAAAAAFfiFo8MmGjHhm3qyr47hhY-hRSB");
        }

        public struct Invisible
        {
            public static string SiteKey => Sitecore.Configuration.Settings.GetSetting("Foundation.Invisible.ReCaptcha.Sitekey", "6Len-DQUAAAAAIi1x4JVNBn9gd4Tb81Yr-twfjVv");
            public static string Secret => Sitecore.Configuration.Settings.GetSetting("Foundation.Invisible.ReCaptcha.Secret", "6Len-DQUAAAAACTW_yPX7A5iwTppYMojUDxmNbc0");
        }
        
    }
}