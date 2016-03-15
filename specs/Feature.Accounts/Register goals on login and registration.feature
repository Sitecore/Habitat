Feature: Register goals on login and registration
	

@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Register goals on login and registration_UC1_Register new user
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email             | Password | ConfirmPassword |
	| kov1@sitecore.net | k        | k               |
	And Actor clicks Register button
	And Actor Ends user visit
	Then Profile Activity Goals section for kov@sitecore.net contains
	| Goal     |
	| Register |
	| Login    |
 
@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Register goals on login and registration_UC2_Login with new user
	Given User is registered in Habitat and logged out
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And Actor selects User icon on Navigation bar
	When Actor enteres following data into Login form fields
	| Email              | Password |
	| kov10@sitecore.net | k        |
	And Actor clicks Login button on User form
	And Actor Ends user visit
	Then Profile Activity Goals section for kov10@sitecore.net contains
	| Goal     |
	| Register |
	| Login    |
	| Login    |


@NeedUpdateStepWithRemovingDataFromAnalytic
Scenario: Account_Register goals on login and registration_UC3_Login twice with new user
	Given User is registered in Habitat and logged out
	| Email              | Password | ConfirmPassword |
	| kov11@sitecore.net | k        | k               |
	When Actor selects User icon on Navigation bar
	And Actor enteres following data into Login form fields
	| Email              | Password |
	| kov11@sitecore.net | k        |
	And Actor clicks Login button on User form
	And Actor selects User icon on Navigation bar
	And Actor clicks Logout button on User form
	And Actor selects User icon on Navigation bar
	And Actor enteres following data into Login form fields
	| Email              | Password |
	| kov11@sitecore.net | k        |
	And Actor clicks Login button on User form
	And Actor Ends user visit
	Then Profile Activity Goals section for kov10@sitecore.net contains
	| Goal     |
	| Register |
	| Login    |
	| Login    |
	| Login    |  