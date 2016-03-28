Feature: Forgot Password



	@Ready
Scenario: Accounts_Forgot Password_UC1_Open Password Resert page on Login Page
	Given Habitat website is opened on Login page
	When Actor clicks Forgot your password? link
	Then Password Reset title presents on ForgotPassword page
	And Page URL ends on /Forgot-Password
	And Following buttons present on Forgot Password page
    | Button name    |
    | Reset password |
	And Forgot password form contains message to user
	| Text on page                                  |
	| The new password will be sent to your e-mail. |


	@Ready
Scenario: Accounts_Forgot Password_UC2_Open Password Resert page on Login form 
	Given User is registered in Habitat and logged out 
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And Actor selects User icon on Navigation bar
	When Actor clicks Forgot your password? button on User form
	Then Password Reset title presents on ForgotPassword page
	And Page URL ends on /Forgot-Password
	And Following buttons present on Forgot Password page
    | Button name    |
    | Reset password |
	And Forgot password form contains message to user
	| Text on page                                  |
	| The new password will be sent to your e-mail. |

	@Ready
Scenario: Accounts_Forgot Password_UC3_Check required fields
	Given Habitat website is opened on Forgot Password page
	When Actor clicks Reset password button on Reset Password page
	Then System shows following error message for the E-mail field
	| Required field error message    |
	| Please enter a value for E-mail |         
	And Page URL ends on /Forgot-Password


	@Ready
Scenario: Accounts_Forgot Password_UC4_Reset password for registered user
	Given User is registered in Habitat and logged out 
	| Email             | Password | ConfirmPassword |
	| kov1@sitecore.net | k        | k               |
	And Habitat website is opened on Register page
	And Habitat website is opened on Forgot Password page
	When Actor enters following data into E-mail field
	| E-mail            |
	| kov1@sitecore.net |
	And Actor clicks Reset password button on Reset Password page
#Some driver issues. This step works well on manual input Then Systen shows following Alert message
	| Info message on Forgot Password page |
	| Your password has been reset.        |
	Then Following buttons is no longer present on Forgot Password page
    | Button name    |
    | Reset Password |
	And Following fields is no longer present on Forgot Password page
    | Field name |
    | Email      |
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
	| E-mail field error message         |
	| Please enter a valid email address |
	And Page URL ends on /Forgot-Password


	@NeedImplementation
Scenario: Accounts_Forgot Password_UC6_Try to reset password for unknown user
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Forgot Password page
	When Actor enters following data into E-mail field
	| E-mail              |
	| kov100@sitecore.net |
	And Actor clicks Reset password button on Reset Password page
	Then System shows following error message for the E-mail field
	| E-mail field error message                        |
	| User with specified e-mail address does not exist |
	
	
	