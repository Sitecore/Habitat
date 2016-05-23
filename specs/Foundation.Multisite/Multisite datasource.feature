@UI
Feature: Multisite datasource
	
@InProgress
Scenario: Multisite datasource_UC1_Standard path
    Given Habitat website is opened on Login page
	And Actor enteres following data into Login page fieldss
	| UserName         | Password |
	| admin			   | b        |
	And Actor clicks Login button
	Given Value set to item field
	| ItemPath                                               | fieldName               | fieldValue                                   |
	| /sitecore/layout/Renderings/Feature/Social/Twitter Feed| Datasource Location	   | /sitecore/content/Habitat/Global/Social	  |
	When User opens Experience Editor 
	And User selects Twitter placeholder
	And User clicks Associate a content item with this component. button on scChromeToolbar undefined
	#Then Following items present in tree
	#| Item name       |
	#| sitecorehabitat | 

#
#@NeedImplementation
#Scenario: Multisite datasource_UC2_Query path
#	Given Value set to item field
#	| ItemPath                                               | Field               | Value                                         |
#	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Location | query:/sitecore/content/Habitat/Global/Social |
#	When User opens Experience Editor 
#	And User selects Twitter placeholder
#	And User clicks <Associate a content item with this component.> button on <scChromeToolbar undefined>
#	Then Following items present in tree
#	| Item name       |
#	| sitecorehabitat |
#
#
#@NeedImplementation
#Scenario: Multisite datasource_UC3_Query with condition
#	Given Value set to item field
#	| ItemPath                                               | Field               | Value                                                            |
#	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Location | query:/sitecore/content//*[@@templatename='Social Feeds Folder'] |
#	When User opens Experience Editor 
#	And User selects Twitter placeholder
#	And User clicks <Associate a content item with this component.> button on <scChromeToolbar undefined>
#	Then Following items present in tree
#	| Item name       |
#	| sitecorehabitat | 
#
#
#@NeedImplementation
#Scenario: Multisite datasource_UC4_Relative path
#	Given Value set to item field
#	| ItemPath                                               | Field               | Value     |
#	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Location | ./Modules |
#	When User opens Experience Editor 
#	And User selects Twitter placeholder
#	And User clicks <Associate a content item with this component.> button on <scChromeToolbar undefined>
#	Then Following items present in tree
#	| Item name  |
#	| Project    |
#	| Feature    |
#	| Foundation |
##Following step just returns system to default value
#	And Value set to item field
#	| ItemPath                                               | Field               | Value                                   |
#	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Location | /sitecore/content/Habitat/Global/Social | 
#
#@NeedImplementation
#Scenario: Multisite datasource_UC5_Item id
#	Given Value set to item field
#	| ItemPath                                               | Field               | Value                                  |
#	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Location | {20507226-1C3B-4D7A-89D4-DE00706E59B6} |
#	When User opens Experience Editor 
#	And User selects Twitter placeholder
#	And User clicks <Associate a content item with this component.> button on <scChromeToolbar undefined>
#	Then Following items present in tree
#	| Item name       |
#	| sitecorehabitat |








	
