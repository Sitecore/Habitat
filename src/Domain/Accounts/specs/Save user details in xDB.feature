Feature: Save user details in xDB
	

@NeedImplementation
Scenario: Account_Save user details in xDB_UC1_Save full info
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Edit Profile page
	And User inputs data in to the fields
	| Last Name | First Name | Phone number    |
	| Maximov   | Stas       | +38(067)8888888 |
	And User selects <Skiing> from Interests drop-down list
	When User clicks Save button
	And User opens MongoDB
	And Following info presents in MongoDB
	| Last Name | First Name | Phone number    | Interests |
	| Teltov    | Konstantin | +38(067)3333333 | Swiming   |


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
	And Following info presents in MongoDB
	| Last Name | First Name | Phone number    | Interests |
	| Maximov   | Stas       | +38(067)8888888 | Skiing    |


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
	And Following info presents in MongoDB
	| Last Name | First Name | Phone number | Interests |
	| Maximov   | empty      | empty        | empty     |
	
	
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
	And Following info presents in MongoDB
	| Last Name | First Name | Phone number    | Interests |
	| empty     | empty      | +38(067)8888888 | empty     |
	
	
	
@NeedImplementation
Scenario: Account_Save user details in xDB_UC5_Save only Interests
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Edit Profile page
	When User selects <Skiing> from Interests drop-down list
	And User clicks Save button
	And User opens MongoDB
	And Following info presents in MongoDB
	| Last Name | First Name | Phone number | Interests |
	| empty     | empty      | empty        | Skiing    |	
	
	

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
	And Following info presents in MongoDB
	| Last Name | First Name | Phone number    | Interests |
	| Maximov   | Konstantin | +38(067)3333333 | Swiming   |
	
	
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
	And Following info presents in MongoDB
	| Last Name | First Name | Phone number    | Interests |
	| Teltov    | Konstantin | +38(067)8888888 | Swiming   | 

@NeedImplementation
Scenario: Account_Save user details in xDB_UC8_Update only only Interests
	Given Habitat user was created and updated with following info
	| Last Name | First Name | Phone number    | Interests |
	| Teltov    | Konstantin | +38(067)3333333 | Swiming   |  
	And Habitat website is opened on Edit Profile page
	When User selects <Skiing> from Interests drop-down list
	And User clicks Save button
	And User opens MongoDB
	And Following info presents in MongoDB
	| Last Name | First Name | Phone number    | Interests |
	| Teltov    | Konstantin | +38(067)3333333 | Skiing    |		  