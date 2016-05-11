@UI
Feature: Define Languages by Site

	As an editor 
	I want to be able to define the supported languages on a per site basis 
	so that different sites can have different languages

@Ready
Scenario: Define Languages by Site_UC1_Set one language on the site
    #Step for manual testing only: Admin user is logged into Content Editor application
    Given en is Selected on the following item
	| ItemPath                  | FieldName           | FieldValue							    |
	| /sitecore/content/Habitat | SupportedLanguages  | {AF584191-45C9-4201-8740-5409F4CF8BDD}  |
	When Habitat website is opened on Main Page
	Then there is no Globe icon in the Main menu on the top of the page


@Ready
Scenario: Define Languages by Site_UC2_No specific languages are defined on the site
	#Step for manual testing only: Admin user is logged into Content Editor application
	Given Following languages defined in Sitecore
	| ItemPath                         |
	| /sitecore/system/Languages/ar-AE |
	| /sitecore/system/Languages/da    |
	| /sitecore/system/Languages/de-AT |
	| /sitecore/system/Languages/en    |
	| /sitecore/system/Languages/fr-BE |
	| /sitecore/system/Languages/ja-JP |
	| /sitecore/system/Languages/ru-RU |
    #Step for manual testing only: When Admin opens following item
	#| ItemPath                  | FieldName            | 
	#| /sitecore/content/Habitat | Supported Languages  |
	When Value set to item field
	| ItemPath                  | FieldName          | FieldValue |
	| /sitecore/content/Habitat | SupportedLanguages |            |
	#Step for manual testing only: And Admin clicks Deselect all link
	#Step for manual testing only: And Admin saves changes on item
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
	#Step for manual testing only:Given Admin user is logged into Content Editor application
	Given Following languages defined in Sitecore
	| Item path                        |
	| /sitecore/system/Languages/ar-BH |
	| /sitecore/system/Languages/da    |
	| /sitecore/system/Languages/en    |
	#Step for manual testing only: When Admin opens following item
	#| ItemPath                  | Field                | 
	#| /sitecore/content/Habitat | Supported Languages  |
	When The following languages have been selected
	| ItemPath                  | FieldName          | FieldValue        |
	| /sitecore/content/Habitat | SupportedLanguages |  {AF584191-45C9-4201-8740-5409F4CF8BDD}\|{2403D44D-A04E-4C4F-B00B-5CE8CF531254}\|{D22B4F9D-21E5-40A2-A99B-7CD22E550D4F}  |
	#Step for manual testing only:And Admin clicks following items in the <All> list
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