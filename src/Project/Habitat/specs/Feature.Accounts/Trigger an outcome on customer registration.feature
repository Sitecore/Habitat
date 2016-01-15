Feature: Trigger an outcome on customer registration
	

@InDesgin
Scenario: Trigger an outcome on customer registration_UC1_Default registration outcome
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	And User clicks on <Info-sign> in the right down corner
	And User clicks END VISIT button
	Then Experince Profile-->Activity outcomes field equals to
	| Outcome        |
	| Marketing Lead |



@InDesgin	
Scenario: Trigger an outcome on customer registration_UC2_Custom registration outcome
	Given Habitat/Accounts analytics/RegisterOutcome field value defined:
	| RegisterOutcome |
	| Sales Lead      |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	And User clicks on <Info-sign> in the right down corner
	And User clicks END VISIT button
	Then Experince Profile-->Activity outcomes field equals to
	| Outcome    |
	| Sales Lead |


@InDesgin
Scenario: Trigger an outcome on customer registration_UC3_None registration outcome
	Given Habitat/Accounts analytics/RegisterOutcome field value defined:
	| RegisterOutcome |
	| None            |
	And Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	And User clicks on <Info-sign> in the right down corner
	And User clicks END VISIT button
	Then Experince Profile-->Activity outcomes field equals to
	| Outcome |
	| Empty   |