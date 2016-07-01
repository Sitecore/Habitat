@UI
Feature: Login

In order to access secure pages
As a website visitor
I want to be able to log in

@Ready
Scenario: Accounts_Login form_UC1_Open Login form
	Given Habitat website is opened on Main Page
	When Actor selects User icon on Navigation bar
	Then Login title presents on Login form
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
Scenario: Aссounts_Login form_UC2_Check required fields
	Given Habitat website is opened on Main Page
	And Actor selects User icon on Navigation bar	
	When Actor clicks Login button on User form
	Then System shows following error message for the Login form
    | Required fields error message     |
    | Please enter a value for E-mail   |
    | Please enter a value for Password |

@Ready
Scenario: Accounts_Login form_UC3_User enters exists Username and Password
	Given User is registered in Habitat and logged out
	| Email               | Password | ConfirmPassword |
	| kovUC3@sitecore.net | k        | k               |
	And Actor selects User icon on Navigation bar	
	When Actor enteres following data into Login form fields
	| Email               | Password |
	| kovUC3@sitecore.net | k        |
	And Actor clicks Login button on User form
	And Actor selects User icon on Navigation bar
	Then Following buttons is no longer present under User icon
    | Button name |
    | Login       |
    | Register    |
	And Following buttons present under User icon
	| Button name  |
	| Edit details |
	| Logout       |
 		


@Ready
Scenario: Accounts_Login form_UC4_Enter exists username and incorrect password
	Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov11@sitecore.net | k        | k               |
	And Actor selects User icon on Navigation bar
	When Actor enteres following data into Login form fields
	| Email              | Password |
	| kov11@sitecore.net | m        |
	And Actor clicks Login button on User form
	Then System shows following error message for the Login form
    | Required fields error message      |
    | Username or password is not valid. |
   

@Ready
Scenario: Accounts_Login form_UC5_Enter invalid email adress 
    Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And Actor selects User icon on Navigation bar
	When Actor enteres following data into Login form fields
	| Email         | Password |
	| hjkkhghkhkghj | k        |
	And Actor clicks Login button on User form
	Then System shows following error message for the Login form
    | Login form error message           |
    | Please enter a valid email address |


@Ready
Scenario: Accounts_Login form_UC6_Enter not exist username and password 
    Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov20@sitecore.net | k        | k               |
	And Actor selects User icon on Navigation bar
	When Actor enteres following data into Login form fields
	| Email              | Password |
	| kov30@sitecore.net | k1       |
	And Actor clicks Login button on User form
	Then System shows following error message for the Login form
    | Login form error message           |
    | Username or password is not valid. |


@Ready
Scenario: Accounts_Login form_UC7_Login with username and password of recently removed user
    Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And User was deleted from the System
	And Actor selects User icon on Navigation bar              
	When Actor enteres following data into Login form fields
	| Email              | Password |
	| kov10@sitecore.net | k        |
	And Actor clicks Login button on User form
	Then System shows following error message for the Login form
    | Login form error message           |
    | Username or password is not valid. |

@Ready
Scenario: Accounts_Login form_UC8_Enter data and close User form 
    Given Habitat website is opened on Main Page
	And Actor selects User icon on Navigation bar
	When Actor enteres following data into Login form fields
	| Email              | Password |
	| kov10@sitecore.net | k        |
	And Actor selects User icon on Navigation bar
	Then Login drop-down popup is no longer presents

	

@Ready
Scenario: Accounts_Login page_UC9_Open Login page
	Given Habitat website is opened on Main Page
	When Actor navigates to Login page 
	Then Login title presents on page
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
	| Register              |  


@Ready
Scenario: Aссounts_Login page_UC10_Check required fields
	Given Habitat website is opened on Login page
	When User clicks Login button on Login page 
	Then System shows following error message for the Login page
    | Required fields error message     |
    | Please enter a value for E-mail   |
    | Please enter a value for Password |


@Ready
Scenario: Accounts_Login page_UC11_Enter correct Username and Password
	Given User is registered in Habitat and logged out 
	| Email             | Password | ConfirmPassword |
	| kov6@sitecore.net | k        | k               |
	And Habitat website is opened on Login page
	When Actor enteres following data into Login page fields
	| Email             | Password |
	| kov6@sitecore.net | k        |
	And User clicks Login button on Login page
	And Actor selects User icon on Navigation bar
	Then Habitat Main page presents
	And Following buttons is no longer present under User icon
    | Button name |
    | Login       |
    | Register    |
	And Following buttons present under User icon
	| Button name |
	| Logout      |
	And Following links present under User popup
	| Link name    |
	| Edit details |


@Ready
Scenario: Accounts_Login page_UC12_Enter exists username and invalid password
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
Scenario: Accounts_Login page_UC13_Enter not exist username and correct password
	Given Habitat website is opened on Login page
	When Actor enteres following data into Login page fields
	| Email                           | Password |
	| kovInvalidusername@sitecore.net | k        |
	And User clicks Login button on Login page
	Then System shows following error message for the Login form
    | Login form error message          |
    | Username or password is not valid.|


@Ready
Scenario: Accounts_Login page_UC14_Enter not exist username and not exist password
    Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Login page
	When Actor enteres following data into Login page fields
	| Email                        | Password |
	| Invalidusername@sitecore.net | k1       |
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
	| Email              | Password |
	| kov10@sitecore.net | k        |
	And User clicks Login button on Login page
	Then System shows following error message for the Login form
    | Login form error message          |
    | Username or password is not valid.|


@Ready
Scenario: Accounts_Login page_UC16_Login from Forgot Password page(bug coverage)
    Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Forgot Password page
	When Actor selects User icon on Navigation bar
	And Actor enteres following data into Login form fields
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Actor clicks Login button on User form
	And Actor selects User icon on Navigation bar
	Then Habitat Main page presents
	And Following buttons is no longer present under User icon
    | Button name |
    | Login       |
    | Register    |
	And Following buttons present under User icon
	| Button name |
	| Logout      |
	And Following links present under User popup
	| Link name    |
	| Edit details |
  