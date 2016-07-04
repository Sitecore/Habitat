@UI
Feature: Log out
	

@Ready 
Scenario: Accounts_Log out_UC1_Simple log out
	Given User with following data is registered in Habitat
	| Email                   | Password | ConfirmPassword |
	| kovLogout1@sitecore.net | k        | k               |
	When Actor selects User icon on Navigation bar
	And Actor clicks Logout button on User form
	And Actor selects User icon on Navigation bar
	Then Habitat Main page presents
	And Following buttons is no longer present under User icon
    | Button name  |
    | Logout       |
	And Following links is no longer present under User popup
	| Link name    |
	| Edit details |
	And Following fields present on User form
	| Field name |
	| Email      |
	| Password   |         	
	And Following buttons present under User icon
	| Login Form Buttons    |
	| Login                 |
	| Forgot your password? |
	| Register              |


@Ready 
Scenario: Accounts_Log out_UC2_Session expired
	Given User with following data is registered in Habitat
	| Email                   | Password | ConfirmPassword |
	| kovLogout2@sitecore.net | k        | k               |
	And Session was expired
	When Actor selects User icon on Navigation bar
	And Actor clicks Logout button on User form
	And Actor selects User icon on Navigation bar
	Then Habitat Main page presents
	And Following buttons is no longer present under User icon
    | Button name  |
    | Logout       |
	And Following links is no longer present under User popup
	| Link name    |
	| Edit details |
	And Following fields present on User form
	| Field name |
	| Email      |
	| Password   |         	
	And Following buttons present under User icon
	| Login Form Buttons    |
	| Login                 |
	| Forgot your password? |
	| Register              |


	 
