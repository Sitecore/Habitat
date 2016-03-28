Feature: Save user details in xDB
	

@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Save user details in xDB_UC1_Save full info
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kovuc1@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email               | Password |
	| kovuc1@sitecore.net | k        |
	And Edit profile page is opened
	When User inputs data in to the fields
	| LastName | FirstName | PhoneNumber     |
	| Maximov  | Stas      | +38(067)3333333 |	
	And User selects Skiing from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 
	Then Contact kovuc1@sitecore.net has FirstName equals Stas and Surname equals Maximov
	And Contact kovuc1@sitecore.net has PhoneNumber equals +38(067)3333333
	And Contact kovuc1@sitecore.net has SMTP emails equals kovuc1@sitecore.net
	And Contact collection Tags.Interests.Values section for kovuc1@sitecore.net consist of
	| "0"    |
	| Skiing |


	




@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Save user details in xDB_UC2_Update full user info
	Given User is registered in Habitat and logged out 
	| Email               | Password | ConfirmPassword |
	| kovuc2@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email               | Password |
	| kovuc2@sitecore.net | k        |
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
	Then Contact kovuc2@sitecore.net has FirstName equals Stas and Surname equals Maximov
	And Contact kovuc2@sitecore.net has PhoneNumber equals +38(067)8888888
	And Contact kovuc2@sitecore.net has SMTP emails equals kovuc2@sitecore.net
	And Contact collection Tags.Interests.Values section for kovuc2@sitecore.net consist of
	| "0"     | "1"    |
	| Swiming | Skiing |





@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Save user details in xDB_UC3_Save only one of the name fields
	Given User is registered in Habitat and logged out 
	| Email               | Password | ConfirmPassword |
	| kovuc3@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email               | Password |
	| kovuc3@sitecore.net | k        |
	And Edit profile page is opened
	When User inputs data in to the fields
	| LastName |
	| Maximov  |
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 
	Then Contact kovuc3@sitecore.net has FirstName equals @empty and Surname equals Maximov
	And Contact kovuc3@sitecore.net has PhoneNumber equals @empty
	And Contact kovuc3@sitecore.net has SMTP emails equals kovuc3@sitecore.net
	And Contact collection Tags.Interests.Values section for kovuc3@sitecore.net consist of
	| "0"    |
	| @empty |
	
	
@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Save user details in xDB_UC4_Save only phone number
	Given User is registered in Habitat and logged out
	| Email               | Password | ConfirmPassword |
	| kovuc4@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email               | Password |
	| kovuc4@sitecore.net | k        |
	And Edit profile page is opened
	When User inputs data in to the fields
	| PhoneNumber     |
	| +38(067)8888888 |
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 	
	Then Contact kovuc4@sitecore.net has FirstName equals @empty and Surname equals @empty
	And Contact kovuc4@sitecore.net has PhoneNumber equals +38(067)8888888
	And Contact kovuc4@sitecore.net has SMTP emails equals kovuc4@sitecore.net
	And Contact collection Tags.Interests.Values section for kovuc4@sitecore.net consist of
	| "0"    |
	| @empty |


	
@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Save user details in xDB_UC5_Save only Interests
	Given User is registered in Habitat and logged out
	| Email               | Password | ConfirmPassword |
	| kovuc5@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email               | Password |
	| kovuc5@sitecore.net | k        |
	And Edit profile page is opened
	When User selects <Skiing> from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit 		
	Then Contact kovuc5@sitecore.net has FirstName equals @empty and Surname equals @empty 
	And Contact kovuc5@sitecore.net has PhoneNumber equals @empty
	And Contact kovuc5@sitecore.net has SMTP emails equals kovuc5@sitecore.net
	And Contact collection Tags.Interests.Values section for kovuc5@sitecore.net consist of
	| "0"    |
	| @empty |


@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Save user details in xDB_UC6_Update only one of the name fields
	Given User is registered in Habitat and logged out
	| Email               | Password | ConfirmPassword |
	| kovuc6@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email               | Password |
	| kovuc6@sitecore.net | k        |
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
	Then Contact kovuc6@sitecore.net has FirstName equals Konstantin and Surname equals Maximov 
	And Contact kovuc6@sitecore.net has PhoneNumber equals +38(067)3333333
	And Contact kovuc6@sitecore.net has SMTP emails equals kovuc6@sitecore.net
	And Contact collection Tags.Interests.Values section for kovuc6@sitecore.net consist of
	| "0"     |
	| Swiming |
	
	
@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Save user details in xDB_UC7_Update only phone number
	Given User is registered in Habitat and logged out
	| Email               | Password | ConfirmPassword |
	| kovuc7@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email               | Password |
	| kovuc7@sitecore.net | k        |
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
	Then Contact kovuc7@sitecore.net has FirstName equals Konstantin and Surname equals Maximov 
	And Contact kovuc7@sitecore.net has PhoneNumber equals +38(067)8888888
	And Contact kovuc7@sitecore.net has SMTP emails equals kovuc7@sitecore.net
	And Contact collection Tags.Interests.Values section for kovuc7@sitecore.net consist of
	| "0"     |
	| Swiming |

@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Save user details in xDB_UC8_Update only only Interests
	Given User is registered in Habitat and logged out
	| Email               | Password | ConfirmPassword |
	| kovuc8@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email               | Password |
	| kovuc8@sitecore.net | k        |
	And User inputs data on User Profile page and clicks Update button
	| LastName | FirstName  | PhoneNumber     | Interests |
	| Teltov   | Konstantin | +38(067)3333333 | Swiming   |
	And Actor Ends user visit
	And Edit profile page is opened  
	When User selects <Skiing> from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit
	Then Contact kovuc8@sitecore.net has FirstName equals Konstantin and Surname equals Teltov 
	And Contact kovuc8@sitecore.net has PhoneNumber equals +38(067)3333333
	And Contact kovuc8@sitecore.net has SMTP emails equals kovuc8@sitecore.net
	And Contact collection Tags.Interests.Values section for kovuc8@sitecore.net consist of
	| "0"     | "1"    |
	| Swiming | Skiing |
	
	
@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Save user details in xDB_UC9_Update system section in xDB
	Given User is registered in Habitat and logged out 
	| Email               | Password | ConfirmPassword |
	| kovuc9@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email               | Password |
	| kovuc9@sitecore.net | k        |
	When Actor Ends user visit
	Then Contact kovuc9@sitecore.net has visit count 2 and value 0  




@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Save user details in xDB_UC10_Empty Interests value 
	Given User is registered in Habitat and logged out 
	| Email                | Password | ConfirmPassword |
	| kovuc10@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email                | Password |
	| kovuc10@sitecore.net | k        |  
	And Edit profile page is opened
	When User selects <Skiing> from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit
	And Edit profile page is opened
	And User selects empty from Interests drop-down list
	And User clicks Update button on Edit User Profile page
	And Actor Ends user visit
	Then Contact collection Tags.Interests.Values section for kovuc10@sitecore.net consist of
	| "0"     | "1"    |
	| Swiming | @empty |
	