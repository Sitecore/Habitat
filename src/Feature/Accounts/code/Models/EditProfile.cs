namespace Sitecore.Feature.Accounts.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Sitecore.Foundation.Dictionary.Repositories;

    public class EditProfile
    {
        [Display(Name = nameof(EmailCaption), ResourceType = typeof(EditProfile))]
        public string Email { get; set; }

        [Display(Name = nameof(FirstNameCaption), ResourceType = typeof(EditProfile))]
        public string FirstName { get; set; }

        [Display(Name = nameof(LastNameCaption), ResourceType = typeof(EditProfile))]
        public string LastName { get; set; }

        [Display(Name = nameof(PhoneNumberCaption), ResourceType = typeof(EditProfile))]
        [RegularExpression(@"^\+?\d*(\(\d+\)-?)?\d+(-?\d+)+$", ErrorMessageResourceName = nameof(PhoneNumberFormat), ErrorMessageResourceType = typeof(EditProfile))]
        [MaxLength(20, ErrorMessageResourceName = nameof(MaxLengthExceeded), ErrorMessageResourceType = typeof(EditProfile))]
        public string PhoneNumber { get; set; }

        [Display(Name = nameof(InterestsCaption), ResourceType = typeof(EditProfile))]
        public string Interest { get; set; }

        public IEnumerable<string> InterestTypes { get; set; }

        public static string EmailCaption => DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/Email", "E-mail");
        public static string FirstNameCaption => DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/First Name", "First name");
        public static string LastNameCaption => DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/Last Name", "Last name");
        public static string PhoneNumberCaption => DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/Phone Number", "Phone number");
        public static string InterestsCaption => DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/Interests", "Interests");
        public static string MaxLengthExceeded => DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/Max Length", "{0} length should be less than {1}");
        public static string PhoneNumberFormat => DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/Phone Number Format", "Phone number should contain only +, ( ) and digits");
    }
}