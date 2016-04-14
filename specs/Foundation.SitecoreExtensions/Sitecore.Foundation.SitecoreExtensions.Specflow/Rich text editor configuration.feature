@UI
Feature: Rich text editor configuration
	

@NeedImplementation
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



@NeedImplementation
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

@NeedImplementation
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