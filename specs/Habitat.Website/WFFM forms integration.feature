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

