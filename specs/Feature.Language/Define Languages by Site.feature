@UI
Feature: Define Languages by Site

	As an editor 
	I want to be able to define the supported languages on a per site basis 
	so that different sites can have different languages

@Ready
Scenario: Define Languages by Site_UC1_Set one language on the site
    #Only for manual testing: Admin user is logged into Content Editor application
    Given en is Selected on the following item
	| ItemPath                  | FieldName            | FieldValue |
	| /sitecore/content/Habitat | Supported Languages  | en         |
	When Habitat website is opened on Main Page
	Then there is no Globe icon in the Main menu on the top of the page


@Ready
Scenario: Define Languages by Site_UC2_No specific languages are defined on the site
	#Only for manual testing: Admin user is logged into Content Editor application
	Given Following languages defined in Sitecore
	| ItemPath                         | FieldName |
	| /sitecore/system/Languages/ar-AE | Iso       |
	| /sitecore/system/Languages/da    | Iso       |
	| /sitecore/system/Languages/de-AT | Iso       |
	| /sitecore/system/Languages/en    | Iso       |
	| /sitecore/system/Languages/fr-BE | Iso       |
	| /sitecore/system/Languages/ja-JP | Iso       |
	| /sitecore/system/Languages/ru-RU | Iso       |
    #Only for manual testing: When Admin opens following item
	| ItemPath                  | FieldName            | 
	| /sitecore/content/Habitat | Supported Languages  |
	When Value set to item field
	| ItemPath                  | FieldName           | FieldValue |
	| /sitecore/content/Habitat | Supported Languages |            |
	#Only for manual testing: And Admin clicks Deselect all link
	#Only for manual testing: And Admin saves changes on item
	And Habitat website is opened on Main Page
	And User clicks Globe icon 
	Then following items available in the list
	| Item name                             |
	| English								|
	| dansk									|
	| français (Belgique)                   |
	| Deutsch (Österreich)                  |
	| 日本語 (日本)                          |
	| (العربية (الإمارات العربية المتحدة	|
	| русский (Россия)                      |


@Ready
Scenario: Define Languages by Site_UC3_Check that language with country code appears in the list
	#Only for manual testing:Given Admin user is logged into Content Editor application
	Given Following languages defined in Sitecore
	| Item path                        |FieldName |
	| /sitecore/system/Languages/ar-BH |	Regional Iso Code       |
	| /sitecore/system/Languages/da    |	Regional Iso Code       |
	| /sitecore/system/Languages/en    |	Regional Iso Code       |
	#Only for manual testing: When Admin opens following item
	#| ItemPath                  | Field                | 
	#| /sitecore/content/Habitat | Supported Languages  |
	When The following languages have been selected
	| ItemPath                  | FieldName           | FieldValue |
	| /sitecore/content/Habitat | Supported Languages |ar-BH	   |
	| /sitecore/content/Habitat | Supported Languages |da		   |
	| /sitecore/content/Habitat | Supported Languages |en		   |
	#Only for manual testing:And Admin clicks following items in the <All> list
	#| Language |
	#| de-AT    |
	#| fr-BE    |
	#And Admin saves changes on item
	And Habitat website is opened on Main Page
	And User clicks Globe icon 
	Then following items available in the list
	| Item name			    |
	| English               |
	| dansk				    |
	| العربية (البحرين)	|
	
@Ready
Scenario: Define Languages by Site_UC4_Switch between languages on the site
	Given Habitat website is opened on Main Page
	When User clicks Globe icon
	And User selects DANSK from the list 
	Then Page URL ends on da/