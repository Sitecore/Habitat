Feature: Page after login
	

@Ready
Scenario: Accounts_Page after login_UC1_Define custom page
	Given User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Value set to item field
	| ItemPath                  | Field          | Value                                     |
	| /Sitecore/Content/Habitat | AfterLoginPage | /sitecore/content/Habitat/Home/Contact Us |
	When Actor opens Habitat website on Login page
	And Actor enteres following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |k        |
	And Actor clicks Login button
	Then Page URL ends on /Contact-Us
	And Contact-Us title presents on page
	  	


@Ready
Scenario: Accounts_Page after login_UC2_Define custom page_defined page does not exists
	Given User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Value set to item field
	| ItemPath                  | Field          | Value                              |
	| /Sitecore/Content/Habitat | AfterLoginPage | /sitecore/content/Habitat/Home/KOV | 
	And Habitat website is opened on Login page
	When Actor enteres following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |k        |
	And Actor clicks Login button
	Then Page URL ends on /en


@Ready
Scenario: Accounts_Page after login_UC3_Define custom page_empty value
	Given User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Value set to item field
	| ItemPath                  | Field          | Value |
	| /Sitecore/Content/Habitat | AfterLoginPage |       | 
	And Habitat website is opened on Login page
	When Actor enteres following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |k        |
	And Actor clicks Login button
	Then Page URL ends on /en

	
@Ready
Scenario: Accounts_Page after login_UC4_After login page for new registered user 
	Given Value set to item field
	| ItemPath                  | Field          | Value                                     |
	| /Sitecore/Content/Habitat | AfterLoginPage | /sitecore/content/Habitat/Home/Contact Us |
	And Habitat website is opened on Register page	
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	Then Page URL ends on /Contact-Us
	And Contact-Us title presents on page


@Ready
Scenario: Accounts_Page after login_UC5_After login behavior for login pop up
	Given Value set to item field
	| ItemPath                  | Field          | Value                                     |
	| /Sitecore/Content/Habitat | AfterLoginPage | /sitecore/content/Habitat/Home/Contact Us |
	And User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               | 
	And Actor moves cursor over the User icon
	And User clicks Login from drop-down menu
	When Actor enteres following data into Login form fields
	| Email            | Password |
	| kov@sitecore.net | k        |
	And User clicks Login button on Login form
	Then Page URL ends on /Contact-Us
	And Contact-Us title presents on page

@Ready
Scenario: Accounts_Page after login_UC6_Double redirect logic
	Given User is registered in Habitat and logged out
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Value set to item field
	| ItemPath                  | Field          | Value                                   |
	| /Sitecore/Content/Habitat | AfterLoginPage | /sitecore/content/Habitat/Home/Register |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	Then Habitat website is opened on Main Page /en