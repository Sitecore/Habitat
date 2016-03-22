Feature: Site switcher
	

@Ready
Scenario: Site switcher_UC1_List of available sites
#	Given Value set to item field
#	| ItemPath               | Field      | Value |
#	| /Sitecore/Content/Demo | ShowInMenu | 1     |
	Given Habitat website is opened on Main Page
	When Actor moves on siteswitcher combo-box
	Then System shows following avalilable sites
	| Site name              |
	| Habitat - Example Site |
	| Demo                   |
	And Habitat - Example Site value is selected by default 

@Ready
Scenario: Site switcher_UC2_Select new site
	Given Habitat website is opened on Main Page
	When Actor moves on siteswitcher combo-box
	And Actor selects Demo from siteswitcher combo-box
	Then URl contains Demo site url
	And Demo site title equals to <Google>



