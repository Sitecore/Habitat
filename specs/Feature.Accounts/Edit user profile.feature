@UI
Feature: Edit user profile
	

@Ready
Scenario: Account_Edit user profile_UC1_Open Edit Profile page
	Given User with following data is registered in Habitat
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	When Actor selects User icon on Navigation bar
	And Actor clicks Edit details button on User form
	Then Update your profile information title presents on page
	And Page URL ends on /Edit-Profile



@Ready
Scenario: Account_Edit user profile_UC2_Update all fields_First time
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Edit profile page is opened
	When User inputs data in to the fields
	| LastName | FirstName  | PhoneNumber     |
	| Teltov   | Konstantin | +38(067)3333333 |
	And User selects Swiming from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	Then Following User info presents for kov@sitecore.net in User Profile
	 | LastName | FirstName  | Phone           | Interest |
	 | Teltov   | Konstantin | +38(067)3333333 | Swiming  |

@Ready
Scenario: Account_Edit user profile_UC3_Update all fields_Change user info
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And User inputs data on User Profile page and clicks Update button
	| LastName | FirstName  | PhoneNumber     | Interests |
	| Teltov   | Konstantin | +38(067)3333333 | Swiming   | 
	When User updates data in to the fields
	| LastName | FirstName | PhoneNumber     |
	| Maximov  | Stas      | +38(067)8888888 |
	And User selects Skiing from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	Then Following User info presents for kov@sitecore.net in User Profile
	 | LastName | FirstName | Phone           | Interest |
	 | Maximov  | Stas      | +38(067)8888888 | Skiing   |
	

@Ready
Scenario: Account_Edit user profile_UC4_Update one of the fields_First time
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Edit profile page is opened
	And All User profile fields are empty
	| FirstName | LastName | PhoneNumber | Interests |
	| empty     | empty    | empty       | empty     |
	When User inputs data in to the fields
	| LastName |
	| Teltov   |
	And User clicks Update button on Edit User Profile page 
	Then Following User info presents for kov@sitecore.net in User Profile
	 | LastName | FirstName | Phone  | Interest |
	 | Teltov   | @empty    | @empty | @empty   |

	  
	   


@Ready
Scenario: Account_Edit user profile_UC5_Update one of the fields_Change user info
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And User inputs data on User Profile page and clicks Update button
	| LastName | FirstName  | PhoneNumber     | Interests |
	| Teltov   | Konstantin | +38(067)3333333 | Swiming   | 
	And Edit profile page is opened
	When User inputs data in to the fields
	| LastName |
	| Maximov  |
	And User clicks Update button on Edit User Profile page  
	Then Following User info presents for kov@sitecore.net in User Profile
	 | LastName | FirstName  | Phone           | Interest |
	 | Maximov  | Konstantin | +38(067)3333333 | Swiming  |


@Ready
Scenario: Account_Edit user profile_UC6_Phone validation_Two plus symbols in the begining
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Edit profile page is opened
	When User inputs data in to the fields
	| PhoneNumber    |
	| ++380673333333 |
	And User clicks Update button on Edit User Profile page
	Then System shows following error message for the Edit Profile
	| Error message                                      |
	| Phone number should contain only +, ( ) and digits | 

@Ready
Scenario: Account_Edit user profile_UC7_Phone validation_Brackets without number 
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Edit profile page is opened
	When User inputs data in to the fields
	| PhoneNumber     |
	| +()380673333333 |
	And User clicks Update button on Edit User Profile page
	Then System shows following error message for the Edit Profile
	| Error message                                      |
	| Phone number should contain only +, ( ) and digits | 


@Ready
Scenario: Account_Edit user profile_UC8_Phone validation_Digits in phone field
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Edit profile page is opened
	When User inputs data in to the fields
	| PhoneNumber  |
	| +38067Kostia |
	And User clicks Update button on Edit User Profile page
	Then System shows following error message for the Edit Profile
	| Error message                                      |
	| Phone number should contain only +, ( ) and digits | 


@Ready
Scenario: Account_Edit user profile_UC9_Phone validation_Phone number lenght should be less than 20
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Edit profile page is opened
	When User inputs data in to the fields
	| PhoneNumber           |
	| +38067333333333333331 |
	And User clicks Update button on Edit User Profile page
	Then System shows following error message for the Edit Profile
	| Error message                              |
	| Phone number lenght should be less than 20 | 


@Ready
Scenario: Account_Edit user profile_UC10_Empty user profile is saved
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Edit profile page is opened
	And All User profile fields are empty
	| FirstName | LastName | PhoneNumber | Interests |
	| empty     | empty    | empty       | empty     |
	And User clicks Update button on Edit User Profile page
	Then Following User info presents for kov@sitecore.net in User Profile
	 | LastName | FirstName | Phone  | Interest |
	 | @empty   | @empty    | @empty | @empty   |