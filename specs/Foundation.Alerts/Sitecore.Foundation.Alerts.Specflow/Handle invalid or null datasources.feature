Feature: Handle invalid or null datasources
	

@InDesign
Scenario: Handle invalid or null datasources_UC1_Experince editor view
	Given Value set to item field
	| ItemPath | FieldName | FieldValue |
	|          |           |            |
	When Habitat website is opened on Main Page in Edit mode
	Then Alert message is shown for item
	|Alert message|



@InDesign
Scenario: Handle invalid or null datasources_UC2_Normal mode
	Given Value set to item field
	| ItemPath | FieldName | FieldValue |
	|          |           |            |
	When Habitat website is opened on Main Page
	Then Item element absents on page
