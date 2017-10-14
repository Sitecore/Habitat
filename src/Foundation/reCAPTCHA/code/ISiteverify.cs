namespace Sitecore.Foundation.reCAPTCHA
{
    public interface ISiteverify
    {
        reCAPTCHAResponse Verify(string reCAPTCHAResponse);
    }
}
