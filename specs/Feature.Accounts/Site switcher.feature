Feature: Site switcher
	

@InDesign
Scenario: Site switcher_UC1_List of available sites
	Given Value set to item field
	| ItemPath               | Field      | Value |
	| /Sitecore/Content/Demo | ShowInMenu | 1     |
	And Habitat website is opened on Main Page
	When Actor clicks on siteswitcher combo-box
	Then System shows following avalilable sites
	| Site name |
	| Habitat   |
	| Demo      |
	And <Habitat> value is selected by default 

@InDesign
Scenario: Site switcher_UC2_Select new site
	Given Habitat website is opened on Main Page
	When Actor clicks on siteswitcher combo-box
	And Actor selects <Demo> from siteswitcher combo-box
	Then Actor present on Demo website


@InDesign
Scenario: Site switcher_UC3_Return to previous site
	Given Habitat website is opened on Main Page
	When Actor clicks on siteswitcher combo-box
	And Actor selects <Demo> from siteswitcher combo-box
	And Actor clicks on siteswitcher combo-box
	And Actor selects <Habitat> from siteswitcher combo-box
	Then Actor present on Habitat website


