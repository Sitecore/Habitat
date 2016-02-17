Feature: Save user details in xDB
	

@NeedImplementation
Scenario: Account_Save user details in xDB_UC1_Save full info
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Edit profile page is opened
	When User inputs data in to the fields
	| LastName | FirstName | PhoneNumber     |
	| Maximov  | Stas      | +38(067)3333333 |	
	And User selects <Skiing> from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 
	Then Contact collection Personal section consist of 
	| FirstName | Surname |
	| Stas      | Maximov |
	And Contact collection Phone Numbers section consist of
	| Number          |
	| +38(067)3333333 |
	And Contact collection Emails section consist of
	| SmtpAddress      |
	| kov@sitecore.net |
	And Contact collection Tags.Interests.Values section consist of
	| "0"    |
	| Skiing |


	




@NeedImplementation
Scenario: Account_Save user details in xDB_UC2_Update full user info
	Given User is registered in Habitat and logged out 
	| Email             | Password | ConfirmPassword |
	| kov2@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email             | Password |
	| kov2@sitecore.net | k        |
	And User inputs data on User Profile page and clicks Update button
	| LastName | FirstName  | PhoneNumber     | Interests |
	| Teltov   | Konstantin | +38(067)3333333 | Swiming   |
	And Actor Ends user visit
	And Edit profile page is opened  
	When User inputs data in to the fields
	| LastName | FirstName | PhoneNumber     |
	| Maximov  | Stas      | +38(067)8888888 |
	And User selects <Skiing> from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 
	Then Contact collection Personal section consist of 
	| FirstName | Surname |
	| Stas      | Maximov |
	And Contact collection Phone Numbers section consist of
	| Number          |
	| +38(067)8888888 |
	And Contact collection Emails section consist of
	| SmtpAddress      |
	| kov@sitecore.net |
	And Contact collection Tags.Interests.Values section consist of
	| "0"     | "1"    |
	| Swiming | Skiing |





@NeedImplementation
Scenario: Account_Save user details in xDB_UC3_Save only one of the name fields
	Given User is registered in Habitat and logged out 
	| Email             | Password | ConfirmPassword |
	| kov3@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email             | Password |
	| kov3@sitecore.net | k        |
	And Edit profile page is opened
	When User inputs data in to the fields
	| LastName |
	| Maximov  |
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 
	Then Contact collection Personal section consist of 
	| FirstName | Surname |
	| empty     | Maximov |
	And Contact collection Phone Numbers section consist of
	| Number |
	| empty  |
	And Contact collection Emails section consist of
	| SmtpAddress      |
	| kov@sitecore.net |
	And Contact collection Tags.Interests.Values section consist of
	| "0"   |
	| empty |
	
	
@NeedImplementation
Scenario: Account_Save user details in xDB_UC4_Save only phone number
	Given User is registered in Habitat and logged out
	| Email             | Password | ConfirmPassword |
	| kov4@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email             | Password |
	| kov4@sitecore.net | k        |
	And Edit profile page is opened
	When User inputs data in to the fields
	| PhoneNumber     |
	| +38(067)8888888 |
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 	
	Then Contact collection Personal section consist of 
	| FirstName | Surname |
	| empty     | empty   |
	And Contact collection Phone Numbers section consist of
	| Number          |
	| +38(067)8888888 |
	And Contact collection Emails section consist of
	| SmtpAddress      |
	| kov@sitecore.net |
	And Contact collection Tags.Interests.Values section consist of
	| "0"   |
	| empty |


	
@NeedImplementation
Scenario: Account_Save user details in xDB_UC5_Save only Interests
	Given User is registered in Habitat and logged out
	| Email             | Password | ConfirmPassword |
	| kov5@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email             | Password |
	| kov5@sitecore.net | k        |
	And Edit profile page is opened
	When User selects <Skiing> from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 		
	Then Contact collection Personal section consist of 
	| FirstName | Surname |
	| empty     | empty   |
	And Contact collection Phone Numbers section consist of
	| Number |
	| empty  |
	And Contact collection Emails section consist of
	| SmtpAddress      |
	| kov@sitecore.net |
	And Contact collection Tags.Interests.Values section consist of
	| "0"   |
	| empty |


@NeedImplementation
Scenario: Account_Save user details in xDB_UC6_Update only one of the name fields
	Given User is registered in Habitat and logged out
	| Email             | Password | ConfirmPassword |
	| kov6@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email             | Password |
	| kov6@sitecore.net | k        |
	And User inputs data on User Profile page and clicks Update button
	| LastName | FirstName  | PhoneNumber     | Interests |
	| Teltov   | Konstantin | +38(067)3333333 | Swiming   |
	And Actor Ends user visit
	And Edit profile page is opened   		
	When User inputs data in to the fields
	| LastName |
	| Maximov  |
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 
	Then Contact collection Personal section consist of 
	| FirstName  | Surname |
	| Konstantin | Maximov |
	And Contact collection Phone Numbers section consist of
	| Number          |
	| +38(067)3333333 |
	And Contact collection Emails section consist of
	| SmtpAddress      |
	| kov@sitecore.net |
	And Contact collection Tags.Interests.Values section consist of
	| "0"     |
	| Swiming |
	
	
@NeedImplementation
Scenario: Account_Save user details in xDB_UC7_Update only phone number
	Given User is registered in Habitat and logged out
	| Email             | Password | ConfirmPassword |
	| kov7@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email             | Password |
	| kov7@sitecore.net | k        |
	And User inputs data on User Profile page and clicks Update button
	| LastName | FirstName  | PhoneNumber     | Interests |
	| Teltov   | Konstantin | +38(067)3333333 | Swiming   |
	And Actor Ends user visit
	And Edit profile page is opened  
	When User inputs data in to the fields
	| Phone number    |
	| +38(067)8888888 |
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 
	Then Contact collection Personal section consist of 
	| FirstName  | Surname |
	| Konstantin | Teltov  |
	And Contact collection Phone Numbers section consist of
	| Number          |
	| +38(067)8888888 |
	And Contact collection Emails section consist of
	| SmtpAddress      |
	| kov@sitecore.net |
	And Contact collection Tags.Interests.Values section consist of
	| "0"     |
	| Swiming |

@NeedImplementation
Scenario: Account_Save user details in xDB_UC8_Update only only Interests
	Given User is registered in Habitat and logged out
	| Email             | Password | ConfirmPassword |
	| kov8@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email             | Password |
	| kov8@sitecore.net | k        |
	And User inputs data on User Profile page and clicks Update button
	| LastName | FirstName  | PhoneNumber     | Interests |
	| Teltov   | Konstantin | +38(067)3333333 | Swiming   |
	And Actor Ends user visit
	And Edit profile page is opened  
	When User selects <Skiing> from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit
	Then Contact collection Personal section consist of 
	| FirstName  | Surname |
	| Konstantin | Teltov  |
	And Contact collection Phone Numbers section consist of
	| Number          |
	| +38(067)3333333 |
	And Contact collection Emails section consist of
	| SmtpAddress      |
	| kov@sitecore.net |
	And Contact collection Tags.Interests.Values section consist of
	| "0"     | "1"    |
	| Swiming | Skiing |
	
	
@NeedImplementation
Scenario: Account_Save user details in xDB_UC9_Update system section in xDB
	Given User is registered in Habitat and logged out 
	| Email             | Password | ConfirmPassword |
	| kov9@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email             | Password |
	| kov9@sitecore.net | k        |
	When Actor Ends user visit
	Then Contact collection System section consist of
	| VisitCount | Value |
	| 2          |       |


@NeedImplementation
Scenario: Account_Save user details in xDB_UC10_Empty Interests value 
	Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email              | Password |
	| kov10@sitecore.net | k        |  
	And Edit profile page is opened
	When User selects <Skiing> from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit
	And Edit profile page is opened
	And User selects empty from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit
	Then Contact collection Tags.Interests.Values section consist of
	| "0"     | "1" |
	| Swiming | ""  |
	