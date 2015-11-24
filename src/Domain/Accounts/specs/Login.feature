Feature: Login

In order to access secure pages
As a website visitor
I want to be able to log in

@NeedImplementation
Scenario: Accounts_Login form_UC1_Open Login form
	Given Habitat website is opened on Main Page
	When Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	Then Login title presents on Login form
	And Following fields present on Login form
	| Field name |
	| Email      |
	| Password   |         	
	And Following buttons present on Login Form
	| Login Form Buttons |
	| Cancel             |
	| Login              |

@NeedImplementation
Scenario: Aссounts_Login form_UC2_Check required fields
	Given Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu	
	When User clicks Login button on Login form
	Then System shows following error message for the Login form
    | Required fields error message |
    | E-mail is required            |
    | Password is required          |


@NeedImplementation
Scenario: Accounts_Login form_UC3_Enter correct Username and Password
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
	And User clicks Login from drop-down menu
	When Actor enteres following data into fields
	| Email              |Password |
	| kov@sitecore.net   |m        |
	And User clicks Login button on Login form
	Then System shows following error message for the Login form
    | Login form error message           |
    | Username or password is not valid. |
   

@NeedImplementation
Scenario: Accounts_Login form_UC5_Enter incorrect username and correct password
    Given User is registered in Habitat
	| Email            | Password |
	| kov@sitecore.net | k        | 
	And Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	When Actor enteres following data into fields
	| Email               |Password |
	| kov1@sitecore.net   |k        |
	And User clicks Login button on Login form
	Then System shows following error message for the Login form
    | Login form error message          |
    | Username or password is not valid |


@NeedImplementation
Scenario: Accounts_Login form_UC6_Enter incorrect username and password
    Given User is registered in Habitat
	| Email            | Password | Confirm password |
	| kov@sitecore.net | k        | k                |
	And Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	When Actor enteres following data into fields
	| Email               |Password  |
	| kov1@sitecore.net   |k1        |
	And User clicks Login button on Login form
	Then System shows following error message for the Login form
    | Login form error message          |
    | Username or password is not valid |


@NeedImplementation
Scenario: Accounts_Login form_UC7_Login with username and password of recently removed user
    Given User is recently deleted from Habitat
	| Email            | Password | 
	| kov@sitecore.net | k        |              
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	When Actor enteres following data into fields
	| E-mail           |Password |
	| kov@sitecore.net |k        |
	And User clicks Login button on Login form
	Then System shows following error message for the Login form
    | Login form error message          |
    | Username or password is not valid |

@NeedImplementation
Scenario: Accounts_Login form_UC8_Click Cancel button on login form
    Given Login form is opened
	When Actor enters following data into fields
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Actor clicks Cancel button
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
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
	| Email      |
	| Password   |         
	And Login button presents
	And Forgot your password? link presents


@NeedImplementation
Scenario: Aссounts_Login page_UC10_Check required fields
	Given Habitat website is opened on Login page
	When Actor enters data in to the following login fields
	| Email | Password |
	|       |          |
	And User clicks Login button
	Then System shows following error message for the Login form
    | Required fields error message |
    | E-mail is required            |
    | Password is required          |


@NeedImplementation
Scenario: Accounts_Login page_UC11_Enter correct Username and Password
	Given User is registered in Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Habitat website is opened on Login page
	When Actor enteres following data into fields
	| Email              |Password |
	| kov@sitecore.net   |k        |
	And Actor clicks Login button
	Then Habitat website openes on Main Page
	And Following buttons present under User drop-drop down menu
	| Button name |
	| Logout      |
	| Edit details|


@NeedImplementation
Scenario: Accounts_Login page_UC12_Enter correct username and incorrect password
	Given User is registered in Habitat
	| Email            | Password |
	| kov@sitecore.net | k        | 
	And Habitat website is opened on Login page
	When Actor enteres following data into fields
	| Email              |Password |
	| kov@sitecore.net   |m        |
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
	| Email             |Password |
	| kov1@sitecore.net |k        |
	And Actor clicks Login button
	Then System shows following error message for the Login form
    | Login form error message          |
    | Username or password is not valid.|


@NeedImplementation
Scenario: Accounts_Login page_UC14_Enter incorrect username and password
    Given User is registered in Habitat
	| Email            | Password |
	| kov@sitecore.net | k        | 
	And Habitat website is opened on Login page
	When Actor enteres following data into fields
	| Email               |Password  |
	| kov1@sitecore.net   |k1        |
	And Actor clicks Login button
	Then System shows following error message for the Login form
    | Login form error message          |
    | Username or password is not valid.|


@NeedImplementation
Scenario: Accounts_Login page_UC15_Login with username and password of recently removed user
    Given User is recently deleted from Habitat
	| Email            | Password | 
	| kov@sitecore.net | k        |    
	And Habitat website is opened on Login page
	When Actor enteres following data into fields
	| Email            |Password |
	| kov@sitecore.net |k        |
	And Actor clicks Login button
	Then System shows following error message for the Login form
    | Login form error message          |
    | Username or password is not valid.|


@Bug35888
Scenario: Accounts_Login page_UC16_Login form fails on Forgot Password page
    Given User is registered in Habitat
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Habitat website is opened on Forgot Password page
	When Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	When Actor enteres following data into fields
	| E-mail           |Password |
	| kov@sitecore.net |k        |
	And Actor clicks Login button
	Then Habitat website openes on Main Page
	And Following buttons present under User drop-drop down menu
	| Button name |
	| Logout      |
	| Edit details|



	 



