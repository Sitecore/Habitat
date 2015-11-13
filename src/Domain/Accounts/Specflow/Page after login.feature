Feature: Page after login
	

@NeedImplementation
Scenario: Accounts_Page after login_UC1_Define custom page
	Given User is registered in Habitat
	| Email            | Password | Confirm password |
	| kov@sitecore.net | k        | k                | 
	And Content Editor is opened by Admin
	And Sitecore/Content/Habitat item is selected
	When User inputs </sitecore/content/Habitat/Home/Contact Us> page in to the AfterLoginPage: field
	And User presses Save button on the Content Editor ribbon
	And User opens Habitat website on Main Page
	And Actor moves cursor over the User icon
	And User selects LOGIN from drop-down menu
	And Actor enteres following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |k        |
	And Actor clicks Login button
	Then Page URL ends on /Contact-Us
	And Contact-Us title presents on page
	  	


@NeedImplementation
Scenario: Accounts_Page after login_UC2_Define custom page_defined page does not exists
	Given User is registered in Habitat
	| Email            | Password | Confirm password |
	| kov@sitecore.net | k        | k                | 
	And Content Editor is opened by Admin
	And Sitecore/Content/Habitat item is selected
	When User inputs </sitecore/content/Habitat/Home/KOV> page in to the AfterLoginPage: field	 
	And User presses Save button on the Content Editor ribbon
	And User clicks Save button on Message dialog popup
	And User opens Habitat website on Main Page
	And Actor moves cursor over the User icon
	And User selects LOGIN from drop-down menu
	And Actor enteres following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |k        |
	And Actor clicks Login button
	Then Page URL ends on /en


@NeedImplementation
Scenario: Accounts_Page after login_UC3_Define custom page_empty value
	Given User is registered in Habitat
	| Email            | Password | Confirm password |
	| kov@sitecore.net | k        | k                | 
	And Content Editor is opened by Admin
	And Sitecore/Content/Habitat item is selected
	When User inputs <empty> page in to the AfterLoginPage: field
	And User presses Save button on the Content Editor ribbon
	And User opens Habitat website on Main Page
	And Actor moves cursor over the User icon
	And User selects LOGIN from drop-down menu
	And Actor enteres following data into fields
	| E-mail              |Password |
	| kov@sitecore.net    |k        |
	And Actor clicks Login button
	Then Page URL ends on /en

	
@NeedImplementation
Scenario: Accounts_Page after login_UC1_After login page for new registered user 
	Given Content Editor is opened by Admin
	And Sitecore/Content/Habitat item is selected
	When User inputs </sitecore/content/Habitat/Home/Contact Us> page in to the AfterLoginPage: field
	And User presses Save button on the Content Editor ribbon
	And User opens Habitat website on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	Then Page URL ends on /Contact-Us
	And Contact-Us title presents on page


