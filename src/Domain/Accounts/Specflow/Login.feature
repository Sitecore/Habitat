Feature: Login

In order to access secure pages
As a website visitor
I want to be able to log in

@NeedImplementation
Scenario: Accounts_Login form_UC1_Open Login form
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
Scenario: Aссounts_Login form_UC2_Check required fields
	Given Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And Actor selects Login from drop-down menu
	And Actor enters data in to the following login fields
	| E-mail | Password |
	|        |          |
	When User clicks Login button
	Then System shows following message for the required fields
    | Required fields error message |
    | E-mail is required            |
    | Password is required          |


@NeedImplementation
Scenario: Accounts_Login form_UC3_Enter correct Username and Password
	Given User is registered in Habitat
	| Email            | Password | 
	| kov@sitecore.net | k        |
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
Scenario: Accounts_Login form_UC4_Enter correct username and incorrect password
	Given User is registered in Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And Actor selects Login from drop-down menu
	When Actor enteres following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |m        |
	And Actor clicks Login button
	Then System shows following message for the Login form
    | Login form error message           |
    | Username or password is not valid. |
   

@NeedImplementation
Scenario: Accounts_Login form_UC5_Enter incorrect username and correct password
    Given User is registered in Habitat
	| Email            | Password |
	| kov@sitecore.net | k        | 
	And Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And Actor selects Login from drop-down menu
	When Actor enteres following data into fields
	| E-mail               |Password |
	| kov1@sitecore.net    |k        |
	And Actor clicks Login button
	Then System shows following message for the Login form
    | Login form error message          |
    | Username or password is not valid |


@NeedImplementation
Scenario: Accounts_Login form_UC6_Enter incorrect username and password
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
    | Login form error message          |
    | Username or password is not valid |


@NeedImplementation
Scenario: Accounts_Login form_UC7_Login with username and password of recently removed user
    Given User is recently deleted from Habitat
	| Email            | Password | 
	| kov@sitecore.net | k        |              
	And Actor moves cursor over the User icon
	And Actor selects Login from drop-down menu
	When Actor enteres following data into fields
	| E-mail           |Password |
	| kov@sitecore.net |k        |
	And Actor clicks Login button
	Then System shows following message for the Login form
    | Login form error message          |
    | Username or password is not valid |

@NeedImplementation
Scenario: Accounts_Login form_UC8_Click Cancel button on login form
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
	

@NeedImplementation
Scenario: Accounts_Login page_UC9_Open Login page
	Given Habitat website is opened on Main Page
	When Actor moves to any secure page
	Then Login title presents on Login page
	And Page URL ends on /Login
	And Following fields present on Login page
	| Field name |
	| E-mail     |
	| Password   |         
	And Login button presents
	And Forgot your password? link presents


@NeedImplementation
Scenario: Aссounts_Login page_UC10_Check required fields
	Given Habitat website is opened on Login page
	When Actor enters data in to the following login fields
	| E-mail | Password |
	|        |          |
	And User clicks Login button
	Then System shows following message for the required fields
    | Required fields error message |
    | E-mail is required            |
    | Password is required          |


@NeedImplementation
Scenario: Accounts_Login page_UC11_Enter correct Username and Password
	Given User is registered in Habitat
	| E-mail            | Password |
	| kov@sitecore.net  | k        |
	And Habitat website is opened on Login page
	When Actor enteres following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |k        |
	And Actor clicks Login button
	Then Habitat website openes on Main Page
	And Following buttons present under User drop-drop down menu
	| Button name |
	| Logout      |
	| Edit details|


@NeedImplementation
Scenario: Accounts_Login page_UC12_Enter correct username and incorrect password
	Given User is registered in Habitat
	| E-mail            | Password |
	| kov@sitecore.net  | k        | 
	And Habitat website is opened on Login page
	When Actor enteres following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |m        |
	And Actor clicks Login button
	Then System shows following message for the Login form
    | Login page error message          |
    | Username or password is not valid.|
   

@NeedImplementation
Scenario: Accounts_Login page_UC13_Enter incorrect username and correct password
    Given User is registered in Habitat
	| Email            | Password | 
	| kov@sitecore.net | k        | 
	And Habitat website is opened on Login page
	When Actor enteres following data into fields
	| E-mail             |Password |
	| kov1@sitecore.net  |k        |
	And Actor clicks Login button
	Then System shows following message for the Login form
    | Login form error message          |
    | Username or password is not valid.|


@NeedImplementation
Scenario: Accounts_Login page_UC14_Enter incorrect username and password
    Given User is registered in Habitat
	| Email            | Password |
	| kov@sitecore.net | k        | 
	And Habitat website is opened on Login page
	When Actor enteres following data into fields
	| E-mail               |Password  |
	| kov1@sitecore.net    |k1        |
	And Actor clicks Login button
	Then System shows following message for the Login form
    | Login form error message          |
    | Username or password is not valid.|


@NeedImplementation
Scenario: Accounts_Login page_UC15_Login with username and password of recently removed user
    Given User is recently deleted from Habitat
	| Email            | Password | 
	| kov@sitecore.net | k        |    
	And Habitat website is opened on Login page
	When Actor enteres following data into fields
	| E-mail           |Password |
	| kov@sitecore.net |k        |
	And Actor clicks Login button
	Then System shows following message for the Login form
    | Login form error message          |
    | Username or password is not valid.|

	



