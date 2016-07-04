@Service
Feature: Rich text editor configuration
	

@OnlyManual
Scenario: Rich text editor configuration_UC1_Default rich text editor defined
	Given Value set to item field
	| ItemPath                                | FieldName | FieldValue |
	| /sitecore/templates/Feature/PageContent | Summary   | @empty     |
	And Experience Editor application is opened
	When User navigates to News page
	And User clicks on News Summary
	And User opens Rich Text Editor
	Then Following classes present in a Rich Text Editor popup
	| Class                       |
	| Bold                        |
	| Italic                      |
	| InsertSitecoreLink          |
	| InsertSitecoreMedia         |
	| Unlink                      |
	| LinkManager                 |
	| InsertSymbol                |
	| PasteFromWordNoFontsNoSizes |
	| Undo                        |
	| Redo                        |
	| FindAndReplace              |



@OnlyManual
Scenario: Rich text editor configuration_UC2_Limitted rich text editor defined
	Given Value set to item field
	| ItemPath                                | FieldName | FieldValue                                                       |
	| /sitecore/templates/Feature/PageContent | Summary   | /sitecore/system/Settings/Html Editor Profiles/Rich Text Limited |
	And Experience Editor application is opened
	When User navigates to News page
	And User clicks on News Summary
	And User opens Rich Text Editor
	Then Following classes present in a Rich Text Editor popup
	| Class                       |
	| Bold                        |
	| Italic                      |
	| InsertSitecoreLink          |
	| InsertSitecoreMedia         |
	| Unlink                      |
	| LinkManager                 |
	| InsertSymbol                |
	| PasteFromWordNoFontsNoSizes |
	| Undo                        |
	| Redo                        |
	| FindAndReplace              |

@OnlyManual
Scenario: Rich text editor configuration_UC3_Complete rich text editor defined
	Given Value set to item field
	| ItemPath                                | FieldName | FieldValue                                                        |
	| /sitecore/templates/Feature/PageContent | Body      | /sitecore/system/Settings/Html Editor Profiles/Rich Text Complete |
	And Experience Editor application is opened
	When User navigates to News page
	And User clicks on News Body
	And User opens Rich Text Editor
	Then Following classes present in a Rich Text Editor popup
	| Class                       |
	| Print                       |
	| FindAndReplace              |
	| Cut                         |
	| Copy                        |
	| Paste                       |
	| PasteFromWord               |
	| PasteFromWordNoFontsNoSizes |
	| PastePlainText              |
	| PasteAsHtml                 |
	| FormatStripper              |
	| Undo                        |
	| Redo                        |
	| InsertSitecoreLink          |
	| InsertSitecoreBucketLink    |
	| InsertSitecoreMedia         |
	| LinkManager                 |
	| Unlink                      |
	| InsertTable                 |
	| InsertParagraph             |
	| InsertDate                  |
	| InsertSnippet               |
	| Bold                        |
	| Italic                      |
	| Underline                   |
	| InsertOrderedList           |
	| InsertUnorderedList         |
	| StrikeThrough               |
	| Subscript                   |
	| Superscript                 |
	| AjaxSpellCheck              |
	| XhtmlValidator              |
	| Help                        |


@Ready
Scenario: Rich text editor configuration_UC4_Assert Custom Html profiles are present
	Then Following items are present under /sitecore/system/Settings/Html Editor Profiles item in Core db
	| Html profile       |
	| Rich Text Complete |
	| Rich Text Limited  |
	| Rich Text Default  | 

@Ready
Scenario: Rich text editor configuration_UC5_Assert Page Content has correct Rich Text Editor values 
	Then Page Content has correct Rich Text Editor sources
	| itemPath                                                                | fieldName | fieldValue                                                        |
	| /sitecore/templates/Feature/PageContent/_HasPageContent/Content/Summary | Source    | /sitecore/system/Settings/Html Editor Profiles/Rich Text Limited  |
	| /sitecore/templates/Feature/PageContent/_HasPageContent/Content/Body    | Source    | /sitecore/system/Settings/Html Editor Profiles/Rich Text Complete |