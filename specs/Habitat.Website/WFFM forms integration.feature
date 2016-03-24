Feature: WFFM forms
	

@NeedImplementation
Scenario: WFFM forms integration_UC1_Verify MVC forms present in all Wide and Narrow columns
	Given User is logged to Sitecore as Admin
	When User selects Content Editor
	Then Items contain fields
	| Item path                                                                 | Allowed Controls |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-1 | MVC Form         |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-2 | MVC Form         |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-3 | MVC Form         |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-4 | MVC Form         |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-5 | MVC Form         |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-6 | MVC Form         |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-wide-1   | MVC Form         |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-wide-2   | MVC Form         |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-wide-3   | MVC Form         |
	
	
	  

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