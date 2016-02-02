Feature: Register

@Ready
Scenario: Accounts_Register_UC1_Open Register page
	Given Habitat website is opened on Main Page
	When Actor moves cursor over the User icon
	And User selects Register from drop-down menu
	Then Page URL ends on /Register
	And Register title presents on page
	And Register fields present on page
	| Field name      |
	| Email           |
	| Password        |
	| ConfirmPassword |	
	And Register button presents

	    
@Ready
Scenario: Accounts_Register_UC2_Register a new user
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	Then Habitat website is opened on Main Page /en
	And Following buttons present under User drop-drop down menu
	| Button name |
	| Logout      |	
	And Following buttons is no longer present under User drop-drop down menu 
	| Button name |
	| Login       |
	| Register    |


@Ready
Scenario: Accounts_Register_UC3_Invalid email
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov$sitecore.net | k        | k               |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Email field error message |
	| Invalid email address     |	
	And Page URL ends on /register 
	
	
@Ready
Scenario: Accounts_Register_UC4_Not unique email
	Given User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Email field error message                |
	| User with specified login already exists |	
	And Page URL ends on /register	
			
	
@Ready
Scenario: Accounts_Register_UC5_Incorrect confirm password
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | a               |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Password field error message |
	| Wrong confirm password       |	
	And Page URL ends on /register
	
	
@Ready
Scenario: Accounts_Register_UC6_One(email) of the required fields is empty
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields with misssed email
	| Password | ConfirmPassword |
	| k        | k               |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Required field error message |
	| E-mail is required           |	
	And Page URL ends on /register
	
@Ready
Scenario: Accounts_Register_UC7_all off the required fields are empty
	Given Habitat website is opened on Register page
	When Actor clicks Register button
	Then System shows following message for the Email field
	| Required field error message |
	| E-mail is required           |
	| Password is required         |	
	And Page URL ends on /register					  