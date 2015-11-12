Feature: Login

In order to access secure pages
As a website visitor
I want to be able to log in

@NeedImplementation
Scenario: Accounts_Login_UC1_Open Login form
	Given Habitat website is opened on Main Page
	When Actor moves cursor over the User icon
	And User selects Login from drop-down menu
	Then Login title presents on Login form
	And Following fields present on Login form
	| Field name |
	| E-mail     |
	| Password   |         
	And Login button presents
	And Cancel button presents


@NeedImplementation
Scenario: Aссounts_Login_UC2_Check required fields
	Given Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And Actor selects Login from drop-down menu
	And Actor enters data in to the following login fields
	| E-mail | Password |
	|        |          |
	When User clicks Login button
	Then System shows following message for the required fields
    | Required fields error message |
    |                               |


@NeedImplementation
Scenario: Accounts_Login_UC3_Enter correct Username and Password
	Given User is registered in Habitat
	| Email            | Password | Confirm password |
	| kov@sitecore.net | k        | k                |
	And Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And Actor selects Login from drop-down menu
	When Actor enteres following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |k        |
	And Actor clicks Login button
	Then Following buttons is no longer present under User drop-drop down menu
    | Button name |
    | Login       |
    | Register    |
	And Following buttons present under User drop-drop down menu
	| Button name |
	| Logout      |	


@NeedImplementation
Scenario: Accounts_Login_UC4_Enter correct username and incorrect password
	Given User is registered in Habitat
	| Email            | Password | Confirm password |
	| kov@sitecore.net | k        | k                |
	And Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And Actor selects Login from drop-down menu
	When Actor enteres following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |m        |
	And Actor clicks Login button
	Then System shows following message for the Login form
    | Login form error message |
    |                          |
   

@NeedImplementation
Scenario: Accounts_Login_UC5_Enter incorrect username and correct password
    Given User is registered in Habitat
	| Email            | Password | Confirm password |
	| kov@sitecore.net | k        | k                |
	And Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And Actor selects Login from drop-down menu
	When Actor enteres following data into fields
	| E-mail               |Password |
	| kov1@sitecore.net    |k        |
	And Actor clicks Login button
	Then System shows following message for the Login form
    | Login form error message |
    |                          |


@NeedImplementation
Scenario: Accounts_Login_UC6_Enter incorrect username and password
    Given User is registered in Habitat
	| Email            | Password | Confirm password |
	| kov@sitecore.net | k        | k                |
	And Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And Actor selects Login from drop-down menu
	When Actor enteres following data into fields
	| E-mail               |Password  |
	| kov1@sitecore.net    |k1        |
	And Actor clicks Login button
	Then System shows following message for the Login form
    | Login form error message |
    |                          |


@NeedImplementation
Scenario: Accounts_Login_UC7_Login with username and password of recently removed user
    Given User is recently deleted from Habitat
	| Email            | Password | Confirm password |
	| kov@sitecore.net | k        | k                |
	And Actor moves cursor over the User icon
	And Actor selects Login from drop-down menu
	When Actor enteres following data into fields
	| E-mail           |Password |
	| kov@sitecore.net |k        |
	And Actor clicks Login button
	Then System shows following message for the Login form
    | Login form error message |
    |                          |

@NeedImplementation
Scenario: Accounts_Login_UC8_Click Cancel button on login form
    Given Login form is opened
	When Actor enters following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |k        |
	And Actor clicks Cancel button
	And Actor moves cursor over the User icon
	And Actor selects Login from drop-down menu
	Then Actor presented with following data in fields
	| E-mail | Password |
	|        |          |
	

