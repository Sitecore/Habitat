Feature: Login

In order to access secure pages
As a website visitor
I want to be able to log in

@Ready
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

@Ready
Scenario: Aссounts_Login form_UC2_Check required fields
	Given Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu	
	When User clicks Login button on Login form
	Then System shows following error message for the Login form
    | Required fields error message |
    | E-mail is required            |
    | Password is required          |


@Ready
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


@Ready
Scenario: Accounts_Login form_UC4_Enter correct username and incorrect password
	Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
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
   

@Ready
Scenario: Accounts_Login form_UC5_Enter incorrect username and correct password
    Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	When Actor enteres following data into fields
	| Email               |Password |
	| kov1@sitecore.net   |k        |
	And User clicks Login button on Login form
	Then System shows following error message for the Login form
    | Login form error message           |
    | Username or password is not valid. |


@Ready
Scenario: Accounts_Login form_UC6_Enter incorrect username and password
    Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And Habitat website is opened on Main Page
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	When Actor enteres following data into fields
	| Email               |Password  |
	| kov1@sitecore.net   |k1        |
	And User clicks Login button on Login form
	Then System shows following error message for the Login form
    | Login form error message           |
    | Username or password is not valid. |


@Ready
Scenario: Accounts_Login form_UC7_Login with username and password of recently removed user
    Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And User was deleted from the System              
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	When Actor enteres following data into fields
	| Email              | Password |
	| kov10@sitecore.net | k        |
	And User clicks Login button on Login form
	Then System shows following error message for the Login form
    | Login form error message           |
    | Username or password is not valid. |

@Ready
Scenario: Accounts_Login form_UC8_Click Cancel button on login form
    Given Login form is opened
	When Actor enteres following data into fields
	| Email              | Password |
	| kov10@sitecore.net | k        |
	And User clicks Cancel button on Login form
	Then Page URL ends on /
	And Following buttons present under User drop-drop down menu
	| Button name |
	| Login       |
	| Register    |
	And Login popup is no longer presents 
	

@Ready
Scenario: Accounts_Login page_UC9_Open Login page
	Given Habitat website is opened on Main Page
	When Actor navigates to Login page 
	Then LOGIN title presents on Login page
	And Following fields present on Login page
	| Field name |
	| Email      |
	| Password   |         	
	And Following buttons present on Login Page
	| Login Page Buttons |
	| Login              |
	And Following links present on Login Page
	| Login page link name  |
	| Forgot your password? |  


@Ready
Scenario: Aссounts_Login page_UC10_Check required fields
	Given Habitat website is opened on Login page
	When User clicks Login button on Login page 
	Then System shows following error message for the Login page
    | Required fields error message |
    | E-mail is required            |
    | Password is required          |


@Ready
Scenario: Accounts_Login page_UC11_Enter correct Username and Password
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Login page
	When Actor enteres following data into Login page fields
	| Email              |Password |
	| kov@sitecore.net   |k        |
	And User clicks Login button on Login page
	Then Habitat Main page presents
	And Following buttons is no longer present under User drop-drop down menu
    | Button name |
    | Login       |
    | Register    |
	And Following buttons present under User drop-drop down menu
	| Button name |
	| Logout      |
	And Following links present under User drop-drop down menu
	| Link name    |
	| Edit details |


@Ready
Scenario: Accounts_Login page_UC12_Enter correct username and incorrect password
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               | 
	And Habitat website is opened on Login page
	When Actor enteres following data into Login page fields
	| Email              |Password |
	| kov@sitecore.net   |m        |
	And User clicks Login button on Login page
	Then System shows following error message for the Login page
    | Login page error message          |
    | Username or password is not valid.|
   

@Ready
Scenario: Accounts_Login page_UC13_Enter incorrect username and correct password
    Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Login page
	When Actor enteres following data into Login page fields
	| Email             |Password |
	| kov1@sitecore.net |k        |
	And User clicks Login button on Login page
	Then System shows following error message for the Login form
    | Login form error message          |
    | Username or password is not valid.|


@Ready
Scenario: Accounts_Login page_UC14_Enter incorrect username and password
    Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Login page
	When Actor enteres following data into Login page fields
	| Email               |Password  |
	| kov1@sitecore.net   |k1        |
	And User clicks Login button on Login page
	Then System shows following error message for the Login form
    | Login form error message          |
    | Username or password is not valid.|


@Ready
Scenario: Accounts_Login page_UC15_Login with username and password of recently removed user
    Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And User was deleted from the System     
	And Habitat website is opened on Login page
	When Actor enteres following data into Login page fields
	| Email            |Password |
	| kov@sitecore.net |k        |
	And User clicks Login button on Login page
	Then System shows following error message for the Login form
    | Login form error message          |
    | Username or password is not valid.|


@Ready
Scenario: Accounts_Login page_UC16_Login form fails on Forgot Password page
    Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Forgot Password page
	When Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	When Actor enteres following data into Login form fields
	| Email            | Password |
	| kov@sitecore.net | k        |
	And User clicks Login button on Login form
	Then Habitat Main page presents
	And Following buttons present under User drop-drop down menu
	| Button name |
	| Logout      |
	| Edit details|



	 



