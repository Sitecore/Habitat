Feature: Register goals on login and registration
	

@NeedImplimentation
Scenario: Account_Register goals on login and registration_UC1_Register new user
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
#Following then steps were taked from Demo-->Show Tracking info feature. 
	And User clicks on <Info-sign> in the right down corner
	And User expands Goals section
	Then Then Following Goals section contains
	| Goal name with score |
	| Register page (0)    |
	| Login (0)            |
 
@NeedImplimentation	
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
	#Following then steps were taked from Demo-->Show Tracking info feature. 
	And User clicks on <Info-sign> in the right down corner
	And User expands Goals section
	Then Then Following Goals section contains
	| Goal name with score |
	| Register page(0)     |
	| Login(0)             |
	| Login(0)             | 


@NeedImplimentation
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
	And User clicks Login from drop-down menu
	And Actor enteres following data into Login form fields
	| Email              | Password |
	| kov10@sitecore.net | k        |
	And User clicks Login button on Login form
	#Following then steps were taked from Demo-->Show Tracking info feature. 
	And User clicks on <Info-sign> in the right down corner
	And User expands Goals section
	Then Then Following Goals section contains
	| Goal name with score |
	| Register page(0)     |
	| Login(0)             |
	| Login(0)             |
	| Login(0)             |  