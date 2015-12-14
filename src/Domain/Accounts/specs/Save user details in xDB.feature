Feature: Save user details in xDB
	

@NeedImplementation
Scenario: Account_Save user details in xDB_UC1_Save full info
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Edit Profile page
	And User inputs data in to the fields
	| Last Name | First Name | Phone number    |
	| Maximov   | Stas       | +38(067)3333333 |
	And User selects <Skiing> from Interests drop-down list
	When User clicks Save button
	And User opens MongoDB
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
	Given Habitat user was created and updated with following info
	| Last Name | First Name | Phone number    | Interests |
	| Teltov    | Konstantin | +38(067)3333333 | Swiming   |  
	And Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Last Name | First Name | Phone number    |
	| Maximov   | Stas       | +38(067)8888888 |
	And User selects <Skiing> from Interests drop-down list
	And User clicks Save button
	And User opens MongoDB
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
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Last Name | 
	| Maximov   |
	And User clicks Save button
	And User opens MongoDB
	Then Contact collection Personal section consist of 
	| Surname |
	| Maximov |
	And Contact collection Emails section consist of
	| SmtpAddress      |
	| kov@sitecore.net |
	And Contact collection Personal section has no fields
	| Field     |
	| Surname   |
	And Contact collection Phone Numbers section has no fields
	| Field  |
	| Number |
	And Contact collection Tags.Interests.Values section has no fields
	| Field |
	| Value |
	
	
@NeedImplementation
Scenario: Account_Save user details in xDB_UC4_Save only phone number
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Phone number    |
	| +38(067)8888888 |
	And User clicks Save button
	And User opens MongoDB		
	Then Contact collection Phone Numbers section consist of
	| Number          |
	| +38(067)8888888 |
	And Contact collection Emails section consist of
	| SmtpAddress      |
	| kov@sitecore.net |
	And Contact collection Personal section has no fields
	| Field     |
	| Surname   |
	| Firstname |
	And Contact collection Tags.Interests.Values section has no fields
	| Field |
	| Value |


	
@NeedImplementation
Scenario: Account_Save user details in xDB_UC5_Save only Interests
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Edit Profile page
	When User selects <Skiing> from Interests drop-down list
	And User clicks Save button
	And User opens MongoDB		
	Then Contact collection Tags.Interests.Values section consist of
	| "0"    |
	| Skiing |
	And Contact collection Emails section consist of
	| SmtpAddress      |
	| kov@sitecore.net |
	And Contact collection Personal section has no fields
	| Field     |
	| Surname   |
	| Firstname |
	And Contact collection Phone Numbers section has no fields
	| Field  |
	| Number |


@NeedImplementation
Scenario: Account_Save user details in xDB_UC6_Update only one of the name fields
	Given Habitat user was created and updated with following info
	| Last Name | First Name | Phone number    | Interests |
	| Teltov    | Konstantin | +38(067)3333333 | Swiming   |  
	And Habitat website is opened on Edit Profile page		
	When User inputs data in to the fields
	| Last Name | 
	| Maximov   |
	And User clicks Save button
	And User opens MongoDB
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
	Given Habitat user was created and updated with following info
	| Last Name | First Name | Phone number    | Interests |
	| Teltov    | Konstantin | +38(067)3333333 | Swiming   |  
	And Habitat website is opened on Edit Profile page
	When User inputs data in to the fields
	| Phone number    |
	| +38(067)8888888 |
	And User clicks Save button
	And User opens MongoDB
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
	Given Habitat user was created and updated with following info
	| Last Name | First Name | Phone number    | Interests |
	| Teltov    | Konstantin | +38(067)3333333 | Swiming   |  
	And Habitat website is opened on Edit Profile page
	When User selects <Skiing> from Interests drop-down list
	And User clicks Save button
	And User opens MongoDB
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
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And Habitat website is opened on Login page
	When Actor enteres following data into Login page fields
	| Email              |Password |
	| kov@sitecore.net   |k        |
	And User clicks Login button on Login page
	And User clicks on <Info-sign> in the right down corner
	And User clicks END VISIT button
	And User opens MongoDB
	Then Contact collection System section consist of
	| VisitCount | Value |
	| 2          |       |


@NeedImplementation
Scenario: Account_Save user details in xDB_UC10_Empty Interests value 
	Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |  
	And Habitat website is opened on Edit Profile page
	When User selects <Skiing> from Interests drop-down list
	And User clicks Save button
	And User selects <Skiing> from Interests drop-down list
	And User clicks Save button
	And User opens MongoDB
	Then Contact collection Tags.Interests.Values section consist of
	| "0"     | "1" |
	| Swiming | ""  |
	