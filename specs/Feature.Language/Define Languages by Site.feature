@UI
Feature: Define Languages by Site

	As an editor 
	I want to be able to define the supported languages on a per site basis 
	so that different sites can have different languages

@Ready
Scenario: Define Languages by Site_UC1_Set one language on the site
    #Step for manual testing only: Admin user is logged into Content Editor application
    Given The following languages have been selected
	| ItemPath                  | FieldName          | FieldValue |
	| /sitecore/content/Habitat | SupportedLanguages | en         |
	When Habitat website is opened on Main Page
	Then there is no Globe icon in the Main menu on the top of the page


@Ready
Scenario: Define Languages by Site_UC2_No specific languages are defined on the site
	#Step for manual testing only: Admin user is logged into Content Editor application			 
    #Step for manual testing only: When Admin opens following item
	#| ItemPath                  | FieldName            | 
	#| /sitecore/content/Habitat | Supported Languages  |
	Given Value set to item field
	| ItemPath                  | FieldName          | FieldValue |
	| /sitecore/content/Habitat | SupportedLanguages |            |
	#Step for manual testing only: And Admin clicks Deselect all link
	#Step for manual testing only: And Admin saves changes on item
	When Habitat website is opened on Main Page
	Then Globe icon was hided

@Ready
Scenario: Define Languages by Site_UC3_Check that language with country code appears in the list
	#Step for manual testing only:Given Admin user is logged into Content Editor application
	Given Following additional languages were defined in Sitecore
	| ItemPath                         | Charset      | CodePage | Encoding | Iso | RegionalIsoCode |
	| /sitecore/system/Languages/ar-BH | windows-1256 | 65001    | utf-8    | ar  | ar-BH           |
	| /sitecore/system/Languages/ja-JP | iso-2022-jp  | 65001    | utf-8    | ja  | ja-JP           |
	| /sitecore/system/Languages/uk-UA | koi8-r       | 65001    | utf-8    | ua  | uk-UA           |
	#Step for manual testing only: When Admin opens following item
	#| ItemPath                  | Field                | 
	#| /sitecore/content/Habitat | Supported Languages  |
	When The following languages have been selected
	| ItemPath                  | FieldName          | FieldValue              |
	| /sitecore/content/Habitat | SupportedLanguages | en;da;ar-BH;ja-JP;uk-UA |
	#Step for manual testing only:And Admin clicks following items in the <All> list
	#| Language |
	#| de-AT    |
	#| fr-BE    |
	#And Admin saves changes on item
	And Habitat website is opened on Main Page
	And User clicks Globe icon 
	Then Following items available in the list
	| Item name            |
	| English              |
	| dansk                |
	| العربية (البحرين)  |
	| 日本語 (日本)         |
	| українська (Україна) |

	
@Ready
Scenario: Define Languages by Site_UC4_Switch between languages on the site
	Given The following languages have been selected
	| ItemPath                  | FieldName          | FieldValue |
	| /sitecore/content/Habitat | SupportedLanguages | en;da      |
	And Habitat website is opened on Main Page
	When User clicks Globe icon
	And User selects DANSK from the list 
	Then Page URL ends on da/