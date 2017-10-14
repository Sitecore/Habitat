namespace Sitecore.Foundation.reCAPTCHA.Configuration
{
    public class Settings
    {
        public static string Sitekey => Sitecore.Configuration.Settings.GetSetting("Foundation.reCAPTCHA.Sitekey", "6LeYNx0TAAAAAM5o86DlvSMR4xSCyu5pz7O0jujG");
        public static string Secret => Sitecore.Configuration.Settings.GetSetting("Foundation.reCAPTCHA.Secret", "6LeYNx0TAAAAAFfiFo8MmGjHhm3qyr47hhY-hRSB");
    }
}