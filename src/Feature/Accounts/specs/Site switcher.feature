Feature: Site switcher
	

@InDesign
Scenario: Site switcher_UC1_List of available sites
	Given Habitat website is opened on Main Page
	When Actor clicks on siteswitcher combo-box
	Then System shows following avalilable sites
	| Site name |
	|           |
	|           |

@InDesign
Scenario: Site switcher_UC2_Select new site
	Given Habitat website is opened on Main Page
	When Actor clicks on siteswitcher combo-box
	And Actor selects <> from siteswitcher combo-box
	Then New website is shown


