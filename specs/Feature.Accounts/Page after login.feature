Feature: Page after login
	

@Ready
Scenario: Accounts_Page after login_UC1_Define custom page
	Given User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Value set to item field
	| ItemPath                  | FieldName      | FieldValue                                          |
	| /Sitecore/Content/Habitat | AfterLoginPage | /sitecore/content/Habitat/Home/Modules/Feature/Demo |
	When Actor opens Habitat website on Login page
	And Actor enteres following data into Login page fields
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Actor clicks Login button
	Then Page URL ends on /Demo
	And Demo title presents on page
	  	


@Ready
Scenario: Accounts_Page after login_UC2_Define custom page_defined page does not exists
	Given User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Value set to item field
	| ItemPath                  | FieldName      | FieldValue                         |
	| /Sitecore/Content/Habitat | AfterLoginPage | /sitecore/content/Habitat/Home/KOV | 
	And Habitat website is opened on Login page
	When Actor enteres following data into Login page fields
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Actor clicks Login button
	Then Page URL not ends on /KOV
	And Habitat Main page presents

@Ready
Scenario: Accounts_Page after login_UC3_Define custom page_empty value
	Given User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Value set to item field
	| ItemPath                  | FieldName      | FieldValue |
	| /Sitecore/Content/Habitat | AfterLoginPage |            | 
	And Habitat website is opened on Login page
	When Actor enteres following data into Login page fields
	| E-mail              |Password |
	| kov@sitecore.net    |k        |
	And Actor clicks Login button
	Then Habitat Main page presents

	
@Ready
Scenario: Accounts_Page after login_UC4_After login page for new registered user 
	Given Value set to item field
	| ItemPath                  | FieldName      | FieldValue                                          |
	| /Sitecore/Content/Habitat | AfterLoginPage | /sitecore/content/Habitat/Home/Modules/Feature/News |
	And Habitat website is opened on Register page	
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	Then Page URL ends on /News
	And News title presents on page


@Ready
Scenario: Accounts_Page after login_UC5_Login pop up returns user to default page
	Given Value set to item field
	| ItemPath                  | FieldName      | FieldValue                              |
	| /Sitecore/Content/Habitat | AfterLoginPage | /sitecore/content/Habitat/Home/Register |
	And User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               | 
	And Actor selects User icon on Navigation bar
	When Actor enteres following data into Login form fields
	| Email            | Password |
	| kov@sitecore.net | k        |
	And Actor clicks Login button on User form
	Then Habitat Main page presents

@Ready
Scenario: Accounts_Page after login_UC6_Double redirect logic for Register page
	Given User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Value set to item field
	| ItemPath                  | FieldName      | FieldValue                              |
	| /Sitecore/Content/Habitat | AfterLoginPage | /sitecore/content/Habitat/Home/Register |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	Then Habitat website is opened on Main Page /en


@Ready
Scenario: Accounts_Page after login_UC7_Double redirect logic for Accounts page
	Given User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Value set to item field
	| ItemPath                  | FieldName      | FieldValue                                              |
	| /Sitecore/Content/Habitat | AfterLoginPage | /sitecore/content/Habitat/Home/Modules/Feature/Accounts |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	Then Habitat website is opened on Main Page /en