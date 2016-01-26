Feature: Trigger an outcome on customer registration
	

@InDesgin
Scenario: Trigger an outcome on customer registration_UC1_Empty registration outcome 
	Given Value set to item field
	| ItemPath                  | Field           | Value |
	| /sitecore/content/Habitat | RegisterOutcome |       |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	And Actor Ends user visit
	Then User Otcome contains value
	| email            | Outcome value |
	| kov@sitecore.net |               |



@InDesgin	
Scenario: Trigger an outcome on customer registration_UC2_Custom registration outcome
	Given Value set to item field
	| ItemPath                  | Field           | Value               |
	| /sitecore/content/Habitat | RegisterOutcome | Outcomes/Sales Lead |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	And Actor Ends user visit
	Then User Otcome contains value
	| email            | Outcome value |
	| kov@sitecore.net | Sales Lead    |


