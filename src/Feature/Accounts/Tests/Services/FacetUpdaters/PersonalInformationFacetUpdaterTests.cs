using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.Accounts.Tests.Services.FacetUpdaters
{
    using FluentAssertions;
    using NSubstitute;
    using Sitecore.Feature.Accounts.Services.FacetUpdaters;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Collection.Model;
    using Sitecore.XConnect.Operations;
    using Xunit;

    public class PersonalInformationFacetUpdaterTests
    {
        private void SetFacets_NoExistingValue_ShouldSetfacet(Contact xdbContact, IXdbContext xdbContext, string profileKey, string value, Func<PersonalInformation, bool> assertValue)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            userProfile[profileKey] = value;
            var facetUpdater = new PersonalInformationFacetUpdater();

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeTrue();
            xdbContext.Received(1).RegisterOperation(Arg.Is<SetFacetOperation<PersonalInformation>>(x =>
                x.FacetReference.FacetKey == PersonalInformation.DefaultFacetKey &&
                assertValue(x.Facet)));
        }

        private void SetFacets_SameValue_ShouldReturnFalse(Contact xdbContact, IXdbContext xdbContext, string profileKey, string value, Action<PersonalInformation> setValue)
        {
            // Arrange
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            userProfile[profileKey] = value;
            var personalInformation = new PersonalInformation();
            setValue(personalInformation);
            Sitecore.XConnect.Serialization.DeserializationHelpers.SetFacet(xdbContact, PersonalInformation.DefaultFacetKey, personalInformation);
            var facetUpdater = new PersonalInformationFacetUpdater();

            // Act
            var changed = facetUpdater.SetFacets(userProfile, xdbContact, xdbContext);

            // Assert
            changed.Should().BeFalse();
            xdbContext.DidNotReceiveWithAnyArgs().RegisterOperation(Arg.Any<IXdbOperation>());
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_NoFirstName_ShouldSetFacet(Contact xdbContact, IXdbContext xdbContext, string name)
        {
            bool AssertValue(PersonalInformation facet) => facet.FirstName == name;
            this.SetFacets_NoExistingValue_ShouldSetfacet(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.FirstName, name, AssertValue);
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_SameFirstName_ShouldReturnFalse(Contact xdbContact, IXdbContext xdbContext, string name)
        {
            void SetValue(PersonalInformation facet) => facet.FirstName = name;
            this.SetFacets_SameValue_ShouldReturnFalse(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.FirstName, name, SetValue);
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_NoMiddleName_ShouldSetFacet(Contact xdbContact, IXdbContext xdbContext, string name)
        {
            bool AssertValue(PersonalInformation facet) => facet.MiddleName == name;
            this.SetFacets_NoExistingValue_ShouldSetfacet(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.MiddleName, name, AssertValue);
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_SameMiddleName_ShouldReturnFalse(Contact xdbContact, IXdbContext xdbContext, string name)
        {
            void SetValue(PersonalInformation facet) => facet.MiddleName = name;
            this.SetFacets_SameValue_ShouldReturnFalse(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.MiddleName, name, SetValue);
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_NoLastName_ShouldSetFacet(Contact xdbContact, IXdbContext xdbContext, string name)
        {
            bool AssertValue(PersonalInformation facet) => facet.LastName == name;
            this.SetFacets_NoExistingValue_ShouldSetfacet(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.LastName, name, AssertValue);
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_SameLastName_ShouldReturnFalse(Contact xdbContact, IXdbContext xdbContext, string name)
        {
            void SetValue(PersonalInformation facet) => facet.LastName = name;
            this.SetFacets_SameValue_ShouldReturnFalse(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.LastName, name, SetValue);
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_NoLanguage_ShouldSetFacet(Contact xdbContact, IXdbContext xdbContext, string language)
        {
            bool AssertValue(PersonalInformation facet) => facet.PreferredLanguage == language;
            this.SetFacets_NoExistingValue_ShouldSetfacet(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.Language, language, AssertValue);
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_SameLanguage_ShouldReturnFalse(Contact xdbContact, IXdbContext xdbContext, string language)
        {
            void SetValue(PersonalInformation facet) => facet.PreferredLanguage = language;
            this.SetFacets_SameValue_ShouldReturnFalse(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.Language, language, SetValue);
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_NoGender_ShouldSetFacet(Contact xdbContact, IXdbContext xdbContext, string gender)
        {
            bool AssertValue(PersonalInformation facet) => facet.Gender == gender;
            this.SetFacets_NoExistingValue_ShouldSetfacet(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.Gender, gender, AssertValue);
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_SameGender_ShouldReturnFalse(Contact xdbContact, IXdbContext xdbContext, string gender)
        {
            void SetValue(PersonalInformation facet) => facet.Gender = gender;
            this.SetFacets_SameValue_ShouldReturnFalse(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.Gender, gender, SetValue);
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_NoBirthday_ShouldSetFacet(Contact xdbContact, IXdbContext xdbContext, DateTime birthday)
        {
            birthday = birthday.Date;
            bool AssertValue(PersonalInformation facet) => facet.Birthdate == birthday;
            this.SetFacets_NoExistingValue_ShouldSetfacet(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.Birthday, birthday.ToLongDateString(), AssertValue);
        }

        [Theory]
        [AutoDbData]
        public void SetFacets_SameBirthday_ShouldReturnFalse(Contact xdbContact, IXdbContext xdbContext, DateTime birthday)
        {
            birthday = birthday.Date;
            void SetValue(PersonalInformation facet) => facet.Birthdate = birthday;
            this.SetFacets_SameValue_ShouldReturnFalse(xdbContact, xdbContext, Accounts.Constants.UserProfile.Fields.Birthday, birthday.ToLongDateString(), SetValue);
        }
    }
}
