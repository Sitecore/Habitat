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
	And User selects Skiing from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 
	Then Contact kov@sitecore.net has FirstName equals Stas and Surname equals Maximov
	And Contact kov@sitecore.net has PhoneNumber equals +38(067)3333333
	And Contact kov@sitecore.net has SMTP emails equals <kov@sitecore.net>
	And Contact collection Tags.Interests.Values section for kov@sitecore.net consist of
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
	Then Contact kov2@sitecore.net has FirstName equals Stas and Surname equals Maximov
	And Contact kov2@sitecore.net has PhoneNumber equals +38(067)8888888
	And Contact kov2@sitecore.net has SMTP emails equals kov2@sitecore.net
	And Contact collection Tags.Interests.Values section for kov2@sitecore.net consist of
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
	Then Contact kov3@sitecore.net has FirstName equals @empty and Surname equals Maximov
	And Contact kov3@sitecore.net has PhoneNumber equals @empty
	And Contact kov3@sitecore.net has SMTP emails equals kov3@sitecore.net
	And Contact collection Tags.Interests.Values section for kov3@sitecore.net consist of
	| "0"    |
	| @empty |
	
	
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
	Then Contact kov4@sitecore.net has FirstName equals @empty and Surname equals @empty
	And Contact kov4@sitecore.net has PhoneNumber equals +38(067)8888888
	And Contact kov4@sitecore.net has SMTP emails equals kov4@sitecore.net
	And Contact collection Tags.Interests.Values section for kov6@sitecore.net consist of
	| "0"    |
	| @empty |


	
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
	Then Contact kov5@sitecore.net has FirstName equals @empty and Surname equals @empty 
	And Contact kov5@sitecore.net has PhoneNumber equals @empty
	And Contact kov4@sitecore.net has SMTP emails equals kov5@sitecore.net
	And Contact collection Tags.Interests.Values section for kov6@sitecore.net consist of
	| "0"    |
	| @empty |


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
	Then Contact kov6@sitecore.net has FirstName equals Konstantin and Surname equals Maximov 
	And Contact kov6@sitecore.net has PhoneNumber equals +38(067)3333333
	And Contact kov6@sitecore.net has SMTP emails equals kov6@sitecore.net
	And Contact collection Tags.Interests.Values section for kov6@sitecore.net consist of
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
	Then Contact kov7@sitecore.net has FirstName equals Konstantin and Surname equals Maximov 
	And Contact kov7@sitecore.net has PhoneNumber equals +38(067)8888888
	And Contact kov7@sitecore.net has SMTP emails equals kov7@sitecore.net
	And Contact collection Tags.Interests.Values section for kov7@sitecore.net consist of
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
	Then Contact kov8@sitecore.net has FirstName equals Konstantin and Surname equals Teltov 
	And Contact kov8@sitecore.net has PhoneNumber equals +38(067)3333333
	And Contact kov8@sitecore.net has SMTP emails equals kov8@sitecore.net
	And Contact collection Tags.Interests.Values section for kov8@sitecore.net consist of
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
	Then Contact kov9@sitecore.net has visit count 2 and value 0  




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
	Then Contact collection Tags.Interests.Values section for kov10@sitecore.net consist of
	| "0"     | "1"    |
	| Swiming | @empty |
	