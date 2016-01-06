Feature: Person_Employee List
	

@InDesign
Scenario: Person_Employee List_UC1_Default datasource defined	
	Given Habitat website is opened on Main Page
	When Actor goes to Person page
	Then Page URL ends on /Modules/Feature/Person 
	And Following persons are shown 
	| Person |
	|        |


@InDesign
Scenario: Person_Employee List_UC2_Custom datasource defined	
	Given Value set to item field
	| ItemPath | Field | Value |
	|          |       |       |
	And Habitat website is opened on Main Page
	When Actor goes to Person page
	Then Page URL ends on /Modules/Feature/Person
	And Following persons are shown 
	| Person |
	|        |
	
	  
