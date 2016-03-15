Feature: WFFM forms
	

@NeedImplementation
Scenario: WFFM forms integration_UC1_Verify MVC forms present in all Wide and Narrow columns
	Given User is logged to Sitecore as Admin
	When User selects Content Editor
	Then MVC form controls presents in all Wide and Narrow columns
	| Column name  |
	| col-narrow-1 |
	| col-narrow-2 |
	| col-narrow-3 |
	| col-narrow-4 |
	| col-narrow-5 |
	| col-narrow-6 |
	| col-wide-1   |
	| col-wide-2   |
	| col-wide-3   |
	
	
	  

@NeedImplementation
Scenario: WFFM forms integration_UC2_Few webforms on one page
	Given New Page was created
	And Webfrom 'Get Our Newsletter' was added to layout	
	And Webfrom 'Leave a message' was added to layout
	And Value set to item field
	| ItemPath           | FieldName        | FieldValue |
	| Get Our Newsletter | Is Ajax Mvc Form | 0          |
	| Leave a message    | Is Ajax Mvc Form | 0          |
	When User inputs data to webForm 
	| WebForm            | FieldName | Field value      |
	| Get Our Newsletter | Email     | kov@sitecore.net |
	Then No form validation errors on page 