Feature: Getting a Site Context Item
	

@InDesign
Scenario: Getting a Site Context Item_UC1_Define custom rendering_Verify item name created
	Given Value set to item field
	| ItemPath                                               | Field               | Value          |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Location | $site:twitter] |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Template |                |	
	When Setting item with Setting template was inserted under /sitecore/content/Habitat
	And RenderingSettings item based on Template {C82DC5FF-09EF-4403-96D3-3CAF377B8C5B} was inserted under /sitecore/content/Habitat/Settings/
	Then Following items present under /sitecore/content/Habitat/Settings
	| Item Name |
	| twitter   |

@InDesign
Scenario: Getting a Site Context Item_UC2_Define custom rendering_List of the available custom items
	Given Value set to item field
	| ItemPath                                               | Field               | Value         |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Location | $site:twitter |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Template |               |
	When Setting item with Setting template was inserted under /sitecore/content/Habitat
	And RenderingSettings item based on Template {C82DC5FF-09EF-4403-96D3-3CAF377B8C5B} was inserted under /sitecore/content/Habitat/Settings/
	And Value set to item field
	| ItemPath                                   | Field              | Value                                                                |
	| /sitecore/content/Habitat/Settings/twitter | DatasourceLocation | /sitecore/content/Habitat/Global/Social                              |
	| /sitecore/content/Habitat/Settings/twitter | DatasourceTemplate | /sitecore/templates/Project/Common/Content Types/Social/Twitter Feed |
	And Twitter Feed item <CustomTwitter> with Twitter Feed template was inserted under /sitecore/content/Habitat/Global/Social/
	And User opens Experience Editor 
	And User selects Twitter placeholder
	And User clicks <Associate a content item with this component.> button on <scChromeToolbar undefined>
	Then Following items present in tree
	| Item name       |
	| sitecorehabitat |
	| CustomTwitter   |


@InDesign
Scenario: Getting a Site Context Item_UC3_Define custom rendering_Apply custom item
	Given Value set to item field
	| ItemPath                                               | Field               | Value         |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Location | $site:twitter |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Template |               |
	When Setting item with Setting template was inserted under /sitecore/content/Habitat
	And RenderingSettings item based on Template {C82DC5FF-09EF-4403-96D3-3CAF377B8C5B} was inserted under /sitecore/content/Habitat/Settings/
	And Value set to item field
	| ItemPath                                   | Field              | Value                                                                |
	| /sitecore/content/Habitat/Settings/twitter | DatasourceLocation | /sitecore/content/Habitat/Global/Social                              |
	| /sitecore/content/Habitat/Settings/twitter | DatasourceTemplate | /sitecore/templates/Project/Common/Content Types/Social/Twitter Feed |
	And Twitter Feed item <CustomTwitter> with Twitter Feed template was inserted under /sitecore/content/Habitat/Global/Social/
	And User opens Experience Editor 
	And User selects Twitter placeholder
	And User clicks <Associate a content item with this component.> button on <scChromeToolbar undefined>
	And User clicks on <CustomTwitter> item and presses OK button on Select the Associated Content popup
	Then <Tweets by @CustomTwitter> text presents on Twitter placeholder


@InDesign
Scenario: Getting a Site Context Item_UC4_Define custom rendering_Item is not available on another site
	Given Value set to item field
	| ItemPath                                               | Field               | Value         |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Location | $site:twitter |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Template |               |
	When Setting item with Setting template was inserted under /sitecore/content/Habitat
	And RenderingSettings item based on Template {C82DC5FF-09EF-4403-96D3-3CAF377B8C5B} was inserted under /sitecore/content/Habitat/Settings/
	And Value set to item field
	| ItemPath                                   | Field              | Value                                                                |
	| /sitecore/content/Habitat/Settings/twitter | DatasourceLocation | /sitecore/content/Habitat/Global/Social                              |
	| /sitecore/content/Habitat/Settings/twitter | DatasourceTemplate | /sitecore/templates/Project/Common/Content Types/Social/Twitter Feed |
	And Twitter Feed item <CustomTwitter> with Twitter Feed template was inserted under /sitecore/content/Habitat/Global/Social/
	And User copies Habitat website
	And User removes Settings under Copy of Habitat 
	And User opens Experience Editor 
	And User selects Twitter placeholder
	And User clicks <Associate a content item with this component.> button on <scChromeToolbar undefined>
	Then Following items present in tree
	| Item name       |
	| sitecorehabitat |



@InDesign
Scenario: Getting a Site Context Item_UC5_Define sitecore rendering_List of the available rendering items
	Given Value set to item field
	| ItemPath                                               | Field               | Value                                   |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Location | query:/sitecore/content/*/Global/Social |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Template | {89D988BC-A9A7-43F5-A9FD-A05B0B164720}  |
	When User opens Experience Editor 
	And User selects Twitter placeholder
	And User clicks <Associate a content item with this component.> button on <scChromeToolbar undefined>
	Then Following items present in tree
	| Item name       |
	| sitecorehabitat |


@InDesign
Scenario: Getting a Site Context Item_UC6_Define sitecore rendering_Datacource settings empty
	Given Value set to item field
	| ItemPath                                               | Field               | Value                                   |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Location | query:/sitecore/content/*/Global/Social |
	| /sitecore/layout/Renderings/Feature/Social/TwitterFeed | Datasource Template | {89D988BC-A9A7-43F5-A9FD-A05B0B164720}  |
	When Setting item with Setting template was inserted under /sitecore/content/Habitat
	And User presses RendringSettings under /sitecore/content/Habitat/Settings
	Then There is no item under following path
	| Item path                      |
	| App Center Sync/Feature/Social |





