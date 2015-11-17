Feature: Log out
	

@NeedImplementation 
Scenario: Accounts_Log out_UC1_Simple log out
	Given User is registered in Habitat
	| Email            |Password | Confirm password |
	| kov@sitecore.net |k        | k                |
	And User kov@sitecore.net is logged to Habitat
	When Actor moves cursor over the User icon
	And User selects LOG OUT from drop-down menu
	Then Visitor presents at Home page
	And Following buttons present under User drop-drop down menu
	| Button name |	
	| Login       |
	| Register    |	
	And Following buttons is no longer present under User drop-drop down menu 
	| Button name |
	| Logout      |


@NeedImplementation 
Scenario: Accounts_Log out_UC2_Session expired
	Given User is registered in Habitat
	| Email            |Password | Confirm password |
	| kov@sitecore.net |k        | k                |
	And User kov@sitecore.net is logged to Habitat
	And  Session was expired
	When Actor moves cursor over the User icon
	And User selects LOG OUT from drop-down menu
	Then Visitor presents at Home page
	And Following buttons present under User drop-drop down menu
	| Button name |	
	| Login       |
	| Register    |	
	And Following buttons is no longer present under User drop-drop down menu 
	| Button name |
	| Logout      |


	 
