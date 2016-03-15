Feature: Register

@Ready
Scenario: Accounts_Register_UC1_Open Register page
	Given Habitat website is opened on Main Page
	When Actor selects User icon on Navigation bar
	And Actor clicks Register button on User form
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
	| Email             | Password | ConfirmPassword |
	| kov3@sitecore.net | k        | k               |
	And Actor clicks Register button
	And Actor selects User icon on Navigation bar
	Then Page URL ends on BaseUrl
	And Following buttons present under User icon
	| Button name  |
	| Logout       |
	| Edit details |	
	And Following buttons is no longer present under User icon 
	| Button name |
	| Login       |
	| Register    |
#And User kov10@sitecore.net presents in User Manager

@Ready
Scenario: Accounts_Register_UC3_Invalid email
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov$sitecore.net | k        | k               |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Email field error message          |
	| Please enter a valid email address |	
	And Page URL ends on /register 
#And User kov10@sitecore.net is not present in User Manager	
	
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
	| Email field error message                           |
	| A user with specified e-mail address already exists |	
	And Page URL ends on /register	
			
	
@Ready
Scenario: Accounts_Register_UC5_Incorrect confirm password
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | a               |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Password field error message                                            |
	| Your password confirmation does not match. Please enter a new password. |	
	And Page URL ends on /register
	
	
@Ready
Scenario: Accounts_Register_UC6_One(email) of the required fields is empty
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields with misssed email
	| Password | ConfirmPassword |
	| k        | k               |	
	And Actor clicks Register button
	Then System shows following message for the Email field
	| Required field error message    |
	| Please enter a value for E-mail |	
	And Page URL ends on /register
	
@Ready
Scenario: Accounts_Register_UC7_all off the required fields are empty
	Given Habitat website is opened on Register page
	When Actor clicks Register button
	Then System shows following message for the Email field
	| Required field error message      |
	| Please enter a value for E-mail   |
	| Please enter a value for Password |	
	And Page URL ends on /register					  