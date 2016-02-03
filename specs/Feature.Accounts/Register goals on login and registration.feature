Feature: Register goals on login and registration
	

@Ready
Scenario: Account_Register goals on login and registration_UC1_Register new user
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	And Actor Ends user visit
	Then Profile Activity Goals section for kov@sitecore.net contains
	| Goal     |
	| Register |
	| Login    |
 
@Ready
Scenario: Account_Register goals on login and registration_UC2_Login with new user
	Given User with following data is registered in Habitat
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And User was logged out from the Habitat
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	When Actor enteres following data into Login form fields
	| Email              | Password |
	| kov10@sitecore.net | k        |
	And User clicks Login button on Login form
	And Actor Ends user visit
	Then Profile Activity Goals section for kov10@sitecore.net contains
	| Goal     |
	| Register |
	| Login    |
	| Login    |


@Ready
Scenario: Account_Register goals on login and registration_UC3_Login twice with new user
	Given User with following data is registered in Habitat
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And User was logged out from the Habitat
	When Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	And Actor enteres following data into Login form fields
	| Email              | Password |
	| kov10@sitecore.net | k        |
	And User clicks Login button on Login form
	And Actor moves cursor over the User icon
	And User clicks Log out on User Icon
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	And Actor enteres following data into Login form fields
	| Email              | Password |
	| kov10@sitecore.net | k        |
	And User clicks Login button on Login form
	And Actor Ends user visit
	Then Profile Activity Goals section for kov10@sitecore.net contains
	| Goal     |
	| Register |
	| Login    |
	| Login    |
	| Login    |  