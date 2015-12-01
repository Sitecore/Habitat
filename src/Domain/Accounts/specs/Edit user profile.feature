Feature: Edit user profile
	

@NeedImplementation
Scenario: Account_Edit user profile_UC1_Open Edit Profile page
	Given User was registered and logged to Habitat
	When Actor moves cursor over the User icon
	And User clicks Edit Profile from drop-down menu
	Then Edit Profile title presents
	And Page URL ends on /Edit Profile

@InDesign
Scenario: Account_Edit user profile_UC2_Update all fields_First time
	Given Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Last Name | First Name | Phone number    |
	| Teltov    | Konstantin | +38(067)3333333 |
	And User selects <Swiming> from Interests drop-down list
	And User clicks Save button
	And User opens Sitecore by Admin user
	And User opens User Manager
	And User clicks on <kov@sitecore.net> in user names list 
	And User presses Edit button on the ribbon 
	And User selects Profile tab on Edit User popup 
	Then Following User info presents 
	 | Last Name | First Name | Phone number    | Interests |
	 | Teltov    | Konstantin | +38(067)3333333 | Swiming   |


@InDesign
Scenario: Account_Edit user profile_UC3_Update all fields_Change user info
	Given Habitat user was created and registered with following info
	| Last Name | First Name | Phone number    | Interests |
	| Teltov    | Konstantin | +38(067)3333333 | Swiming   | 
	And Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Last Name | First Name | Phone number    |
	| Maximov   | Stas       | +38(067)8888888 |
	And User selects <Skiing> from Interests drop-down list
	And User clicks Save button
	And User opens Sitecore by Admin user
	And User opens User Manager
	And User clicks on <kov@sitecore.net> in user names list 
	And User presses Edit button on the ribbon 
	And User selects Profile tab on Edit User popup 
	Then Following User info presents 
	 | Last Name | First Name | Phone number    | Interests |
	 | Maximov   | Stas       | +38(067)8888888 | Skiing    |
	

@InDesign
Scenario: Account_Edit user profile_UC4_Update one of the fields_First time
	Given Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Last Name |
	| Teltov    | 
	And User clicks Save button
	And User opens Sitecore by Admin user
	And User opens User Manager
	And User clicks on <kov@sitecore.net> in user names list 
	And User presses Edit button on the ribbon 
	And User selects Profile tab on Edit User popup 
	Then Following User info presents 
	 | Last Name |
	 | Teltov    |
	 And Following user info absents
	  | First Name | Phone number | Interests |
	  | empty      | empty        | empty     | 


@InDesign
Scenario: Account_Edit user profile_UC5_Update one of the fields_Change user info
	Given Habitat user was created and registered with following info
	| Last Name | First Name | Phone number    | Interests |
	| Teltov    | Konstantin | +38(067)3333333 | Swiming   | 
	And Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Last Name |
	| Maximov   |  
	And User clicks Save button
	And User opens Sitecore by Admin user
	And User opens User Manager
	And User clicks on <kov@sitecore.net> in user names list 
	And User presses Edit button on the ribbon 
	And User selects Profile tab on Edit User popup 
	Then Following User info presents 
	| Last Name | First Name | Phone number    | Interests |
	| Maximov   | Konstantin | +38(067)3333333 | Swiming   |


@InDesign
Scenario: Account_Edit user profile_UC6_Phone validation_Two plus symbols in the begining
	Given Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Phone number   |
	| ++380673333333 |
	And User clicks Save button
	Then System shows following error message for the Edit Profile
	| Error message                                      |
	| Phone number should contain only +, ( ) and digits | 

@InDesign
Scenario: Account_Edit user profile_UC6_Phone validation_Brackets without number 
	Given Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Phone number    |
	| +()380673333333 |
	And User clicks Save button
	Then System shows following error message for the Edit Profile
	| Error message                                      |
	| Phone number should contain only +, ( ) and digits | 


@InDesign
Scenario: Account_Edit user profile_UC6_Phone validation_Digits in phone field
	Given Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Phone number |
	| +38067Kostia |
	And User clicks Save button
	Then System shows following error message for the Edit Profile
	| Error message                                      |
	| Phone number should contain only +, ( ) and digits | 


@InDesign
Scenario: Account_Edit user profile_UC6_Phone validation_Phone number lenght should be less than 20
	Given Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Phone number          |
	| +38067333333333333331 |
	And User clicks Save button
	Then System shows following error message for the Edit Profile
	| Error message                              |
	| Phone number lenght should be less than 20 | 


@InDesign
Scenario: Account_Edit user profile_UC7_Empty user profile is saved
	Given Habitat website is opened on Edit Profile page
	When User clicks Save button
	And User opens Sitecore by Admin user
	And User opens User Manager
	And User clicks on <kov@sitecore.net> in user names list 
	And User presses Edit button on the ribbon 
	And User selects Profile tab on Edit User popup 
	Then No any user profile fields present