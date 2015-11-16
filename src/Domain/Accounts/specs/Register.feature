Feature: Register

@NeedImplementation
Scenario: Accounts_Register_UC1_Open Register page
	Given Habitat website is opened on Main Page
	When Actor moves cursor over the User icon
	And User selects REGISTER from drop-down menu
	Then Page URL ends on /register
	And Register title presents on Register page
	And Register fields present on Register page
	| Field name       |
	| Email            |
	| Login            |
	| Password         |
	| Confirm password |	
	And Register button presents

	    
@NeedImplementation
Scenario: Accounts_Register_UC2_Register a new user
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Login | Password | Confirm password |
	| kov@sitecore.net | kov   | k        | k                |	
	And Actor clicks Register button
	Then Habitat website is opened on Main Page
	And Following buttons present under User drop-drop down menu
	| Button name |
	| Logout      |	
	And Following buttons is no longer present under User drop-drop down menu 
	| Button name |
	| Login       |
	| Register    |


@InDesign
Scenario: Accounts_Register_UC3_Invalid email
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Login | Password | Confirm password |
	| kov$sitecore.net | kov   | k        | k                |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Email field error message |
	|                           |	
	And Habitat website is opened on Register page 
	
	
@InDesign
Scenario: Accounts_Register_UC4_Not unique email
	Given User with following data is registered
	| Email            | Login | Password | Confirm password |
	| kov@sitecore.net | kov   | k        | k                |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Login | Password | Confirm password |
	| kov@sitecore.net | kost  | k        | k                |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Email field error message |
	|                           |	
	And Habitat website is opened on Register page	
	
	
@InDesign
Scenario: Accounts_Register_UC5_Not unique login
	Given User with following data is registered
	| Email            | Login | Password | Confirm password |
	| kov@sitecore.net | kov   | k        | k                |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email             | Login | Password | Confirm password |
	| kost@sitecore.net | kov   | k        | k                |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Login field error message |
	|                           |	
	And Habitat website is opened on Register page	
	
	
@InDesign
Scenario: Accounts_Register_UC6_Incorrect confirm password
	Given User with following data is registered
	| Email            | Login | Password | Confirm password |
	| kov@sitecore.net | kov   | k        | k                |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Login | Password | Confirm password |
	| kov@sitecore.net | kov   | k        | k                |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Password field error message |
	|                           |	
	And Habitat website is opened on Register page
	
	
@InDesign
Scenario: Accounts_Register_UC7_One of the required fields is empty
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email | Login | Password | Confirm password |
	|       | kov   | k        | k                |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Required field error message |
	|                              |	
	And Habitat website is opened on Register page
	
@InDesign
Scenario: Accounts_Register_UC8_all off the required fields are empty
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email | Login | Password | Confirm password |
	|       |       |          |                  |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Required field error message |
	|                              |	
	And Habitat website is opened on Register page					  