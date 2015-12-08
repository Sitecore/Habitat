Feature: Forgot Password



	@Ready
Scenario: Accounts_Forgot Password_UC1_Open Password Resert page on Login Page
	Given Habitat website is opened on Login page
	When Actor clicks Forgot your password? link
	Then Password Reset title presents on ForgotPassword page
	And Page URL ends on /ForgotPassword
	And Forgot password form contains message to user
	| Text on page                                  |
	| The new password will be sent to your e-mail. |


	@Ready
Scenario: Accounts_Forgot Password_UC2_Open Password Resert page on Login form 
	Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And Login form is opened
	When Actor clicks Forgot your password? link
	Then Password Reset title presents on ForgotPassword page
	And Page URL ends on /ForgotPassword
	And Forgot password form contains message to user
	| Text on page                                  |
	| The new password will be sent to your e-mail. |



	@Ready
Scenario: Accounts_Forgot Password_UC3_Check required fields
	Given Habitat website is opened on Forgot Password page
	When Actor clicks Reset password button on Reset Password page
	Then System shows following error message for the E-mail field
	| Required field error message  |
	| E-mail is required            |
	And Page URL ends on /ForgotPassword


	@Ready
Scenario: Accounts_Forgot Password_UC4_Reset password for registered user
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Forgot Password page
	When Actor enters following data into E-mail field
	| E-mail          |
	| kov@sitecore.net|
	And Actor clicks Reset password button on Reset Password page
	Then Systen shows following Alert message
	| Info message on Forgot Password page |
	| Your password has been reset.        |
	And Then Following buttons is no longer present on Forgot Password page
    | Button name    |
    | Reset Password |
	Then Following fields is no longer present on Forgot Password page
    | Field name  |
    | E-mail      |
#	And User receives an e-mail with new password
#	And User can login with new email

	
	@Ready
Scenario: Accounts_Forgot Password_UC5_Invalid e-mail
	Given Habitat website is opened on Forgot Password page
	When Actor enters following data into E-mail field 
	| E-mail           |
	| kov$sitecore.net |
	And Actor clicks Reset password button on Reset Password page
	Then System shows following error message for the E-mail field
	| E-mail field error message |
	| Invalid email address      |
	And Page URL ends on /ForgotPassword


	@NeedImplementation
Scenario: Accounts_Forgot Password_UC6_Try to reset password for unknown user
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Forgot Password page
	When Actor enters following data into E-mail field
	| E-mail            |
	| kov1@sitecore.net |
	And Actor clicks Reset password button on Reset Password page
	Then System shows following error message for the E-mail field
	| E-mail field error message               |
	| User with specified email does not exist |
	
	
	