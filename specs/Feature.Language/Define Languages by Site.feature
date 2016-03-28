Feature: Define Languages by Site

	As an editor 
	I want to be able to define the supported languages on a per site basis 
	so that different sites can have different languages

@NeedImplementation
Scenario: Define Languages by Site_UC1_Set one language on the site
	Given <en> is Selected on the following item
	| ItemPath                  | Field                | 
	| /sitecore/content/Habitat | Supported Languages  | 
	When User opens Habitat website
	Then there is no Globe icon in the Main menu on the top of the page


@NeedImplementation
Scenario: Define Languages by Site_UC2_No specific languages are defined on the site
	Given Admin user is logged into Content Editor application
	And Following languages defined in Sitecore
	| Item path                        |
	| /sitecore/system/Languages/ar-AE |
	| /sitecore/system/Languages/da    |
	| /sitecore/system/Languages/de-AT |
	| /sitecore/system/Languages/en    |
	| /sitecore/system/Languages/fr-BE |
	| /sitecore/system/Languages/ja-JP |
	| /sitecore/system/Languages/ru-RU |
	When Admin opens following item
	| ItemPath                  | Field                | 
	| /sitecore/content/Habitat | Supported Languages  |
	And Admin clicks <Deselect all> link
	And Admin saves changes on item
	And User opens Habitat website
	And User clicks Globe icon 
	Then following items available in the list
	| Item name                             |
	| English                               |
	| dansk                                 |
	| français (Belgique)                   |
	| Deutsch (Österreich)                  |
	| 日本語 (日本)                          |
	| (العربية (الإمارات العربية المتحدة |
	| русский (Россия)                      |


@NeedImplementation
Scenario: Define Languages by Site_UC3_Check that language with country code appears in the list
	Given Admin user is logged into Content Editor application
	And Following languages defined in Sitecore
	| Item path                        |
	| /sitecore/system/Languages/ar-AE |
	| /sitecore/system/Languages/da    |
	| /sitecore/system/Languages/de-AT |
	| /sitecore/system/Languages/en    |
	| /sitecore/system/Languages/fr-BE |
	| /sitecore/system/Languages/ja-JP |
	| /sitecore/system/Languages/ru-RU |
	When Admin opens following item
	| ItemPath                  | Field                | 
	| /sitecore/content/Habitat | Supported Languages  |
	And Admin clicks following items in the <All> list
	| Language |
	| de-AT    |
	| fr-BE    |
	And Admin saves changes on item
	And User opens Habitat website
	And User clicks Globe icon 
	Then following items available in the list
	| Item name             |
	| français (Belgique)   |
	| Deutsch (Österreich)  |
	
@NeedImplementation
Scenario: Define Languages by Site_UC4_Switch between languages on the site
	Given Habitat website is opened
	When User clicks Globe icon
	And User selects DANSK from the list 
	Then site language is switched to danish