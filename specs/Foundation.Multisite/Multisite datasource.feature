@UI
Feature: Multisite datasource
	
@Ready
Scenario: Multisite datasource_UC1_Standard path
	Given Value set to item field
	| ItemPath                                               | fieldName               | fieldValue                                   |
	| /sitecore/layout/Renderings/Feature/Social/Twitter Feed| Datasource Location	   | /sitecore/content/Habitat/Global/Social	  |
	And Experience Editor is opened on Social Page 
	When User selects Twitter placeholder
	And User clicks Associate a content item with this component. button on scChromeToolbar undefined
	Then Following items present in datasource tree
	| Item name       |
	| sitecorehabitat | 


@Ready
Scenario: Multisite datasource_UC2_Query path
	Given Value set to item field
	| ItemPath                                                | fieldName           | fieldValue                                    |
	| /sitecore/layout/Renderings/Feature/Social/Twitter Feed | Datasource Location | query:/sitecore/content/Habitat/Global/Social |
	And Experience Editor is opened on Social Page 
	When User selects Twitter placeholder
	And User clicks Associate a content item with this component. button on scChromeToolbar undefined
	Then Following items present in datasource tree
	| Item name       |
	| sitecorehabitat |


@Ready
Scenario: Multisite datasource_UC3_Query with condition
	Given Value set to item field
	| ItemPath                                                | fieldName           | fieldValue                                                       |
	| /sitecore/layout/Renderings/Feature/Social/Twitter Feed | Datasource Location | query:/sitecore/content//*[@@templatename='Social Feeds Folder'] |
	And Experience Editor is opened on Social Page 
	When User selects Twitter placeholder
	And User clicks Associate a content item with this component. button on scChromeToolbar undefined
	Then Following items present in datasource tree
	| Item name       |
	| sitecorehabitat | 


@Ready
Scenario: Multisite datasource_UC4_Relative path has defined
	Given Value set to item field
	| ItemPath                                                             | fieldName           | fieldValue |
	| /sitecore/layout/Renderings/Feature/Media/Page Header Media Carousel | Datasource Location | ./Modules  |
	And Experience Editor is opened on Main Page
	When User selects Page Header Media Carousel placeholder
	And User clicks Associate a content item with this component. button on scChromeToolbar undefined
	Then Following items present in datasource tree
	| Item name  |
	| Project    |
	| Feature    |
	| Foundation |
#Following step just returns system to default value
	And Value set to item field
	| ItemPath                                                | fieldName           | fieldValue    |
	| /sitecore/layout/Renderings/Feature/Social/Twitter Feed | Datasource Location | site:carousel |

@Ready
Scenario: Multisite datasource_UC5_Item id
	Given Value set to item field
	| ItemPath                                                | fieldName           | fieldValue                             |
	| /sitecore/layout/Renderings/Feature/Social/Twitter Feed | Datasource Location | {20507226-1C3B-4D7A-89D4-DE00706E59B6} |
	And Experience Editor is opened on Social Page 
	When User selects Twitter placeholder
	And User clicks Associate a content item with this component. button on scChromeToolbar undefined
	Then Following items present in datasource tree
	| Item name       |
	| sitecorehabitat |
	#Following step just returns system to default value
	And Value set to item field
	| ItemPath                                                | fieldName           | fieldValue                              |
	| /sitecore/layout/Renderings/Feature/Social/Twitter Feed | Datasource Location | /sitecore/content/Habitat/Global/Social |







	
