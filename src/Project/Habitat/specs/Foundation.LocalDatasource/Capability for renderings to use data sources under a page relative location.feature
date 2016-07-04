@UI
Feature: Capability for renderings to use data sources under a page relative location
	

@mytag
Scenario: Capability for renderings to use data sources under a page relative location
	Given Experience Editor is opened on Social Page
	When User selects Twitter placeholder
	And User clicks Associate a content item with this component. button on scChromeToolbar undefined
	And User selects <Clone Current Content> tab
	And User clicks OK button on popup
	And User selects Twitter placeholder
	And User clicks Associate a content item with this component. button on scChromeToolbar undefined
	Then Following items present in datasource tree under Local Content 	
	And Local content rendering presents under </sitecore/content/Habitat/Home/Modules/Feature/Social/_Local> 
	| Item name       |
	| sitecorehabitat |
	
