Feature: Log out
	

@Ready 
Scenario: Accounts_Log out_UC1_Simple log out
	Given User with following data is registered in Habitat
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	When Actor moves cursor over the User icon
	And User clicks Log out on User Icon 
	Then Habitat Main page presents
	And Following buttons is no longer present under User drop-drop down menu
    | Button name |
    | Logout      |
	And Following buttons present under User drop-drop down menu
	| Button name |
	| Login       |
	| Register    |


@NeedImplementation 
Scenario: Accounts_Log out_UC2_Session expired
	Given User with following data is registered in Habitat
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And Session was expired
	When Actor moves cursor over the User icon
	And User clicks Log out on User Icon
	Then Habitat Main page presents
	And Following buttons is no longer present under User drop-drop down menu
    | Button name |
    | Logout      |
	And Following buttons present under User drop-drop down menu
	| Button name |
	| Login       |
	| Register    |


	 
