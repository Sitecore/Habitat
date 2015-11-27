Feature: Forgot Password

In order to be able to login 
As a site visitor
I want to be able to restore password

	@NeedImplementation
Scenario: Accounts_Forgot Password_UC1_Open Password Resert page
	Given Habitat website is opened on Login Page
	When Actor clicks Forgot your password? link
	Then Password reset title presents on Forgot Password page
	And Page URL ends on /ForgotPassword
	And following text is present on the page
	| Text on page                                  |
	| The new password will be sent to your e-mail. |


	@NeedImplementation
Scenario: Accounts_Forgot Password_UC2_Check required fields
	Given Habitat website is opened on Forgot Password Page
	When Actor clicks Reset Password button
	Then System shows following error message for the E-mail field
	| Required field error message  |
	| E-mail is required            |
	And Page URL ends on /ForgotPassword


	@NeedImplementation
Scenario: Accounts_Forgot Password_UC3_Reset password for registered user
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Forgot Password Page
	When Actor enters following data into E-mail field
	| E-mail          |
	| kov@sitecore.net|
	And Actor clicks Reset Password button
	Then Systen shows following message
	| Info message on Forgot Password page |
	| Your password has been reset.        |
	And Then Following buttons is no longer present on Forgot Password page
    | Button name    |
    | Reset Password |
	Then Following fields is no longer present on Forgot Password page
    | Field name  |
    | E-mail      |
	And User receives an e-mail with new password

	
	@NeedImplementation
Scenario: Accounts_Forgot Password_UC4_Invalid e-mail
	Given Habitat website is opened on Forgot Password Page
	When Actor enters following text to E-mail field
	| E-mail           |
	| kov$sitecore.net |
	And Actor clicks Reset Password button
	Then System shows following error message for the E-mail field
	| E-mail field error message |
	| Invalid email address      |
	And Page URL ends on /ForgotPassword


	@NeedImplementation
Scenario: Accounts_Forgot Password_UC5_Try to reset password for unknown user
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Forgot Password Page
	When Actor enters following data into E-mail field
	| E-mail            |
	| kov1@sitecore.net |
	And Actor clicks Reset Password button
	Then Systen shows following message
	| E-mail field error message               |
	| User with specified email does not exist |
	
	
	