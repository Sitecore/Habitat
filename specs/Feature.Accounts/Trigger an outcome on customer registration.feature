Feature: Trigger an outcome on customer registration
	

@Ready
Scenario: Trigger an outcome on customer registration_UC1_Empty registration outcome 
	Given Value set to item field
	| ItemPath                  | FieldName       | FieldValue |
	| /sitecore/content/Habitat | RegisterOutcome |            |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	And Actor Ends user visit
	Then User Outcome contains value
	| email            | Outcome value |
	| kov@sitecore.net |               |



@Ready	
Scenario: Trigger an outcome on customer registration_UC2_Custom registration outcome
	Given Outcome set to item field
	| ItemPath                  | FieldName       | FieldValue          |
	| /sitecore/content/Habitat | RegisterOutcome | Outcomes/Sales Lead |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	And Actor Ends user visit
	And Wating for timeout 30 s
	Then User Outcome contains value
	| email            | Outcome value |
	| kov@sitecore.net | Sales Lead    |


