Feature: Mockup of External page

In order to start a demo story from an external page
As a technical presales consultant
I want to be able to show a mockup of an external page, e.g. search engine page with adword links to a campaign on the website

	@NeedImplementation
Scenario: Demo_Show Google search engine mockup
	Given browser is opened in inPrivate mode
	When Actor follows http://demo.habitat.test5ua1.dk.sitecore.net/ link
	Then Mockup of Google page is opened
	And Following buttons are present on the page
	| Goggle page buttons |
	| Google Search       |
	| I'm feeling Lucky   |

	
	@NeedImplementation
Scenario: Demo_Show predefined keyword
	Given Mockup of Google page is opened
	When Actor enters any text to search field
	Then search field contains following text
	| Text             |
	| Sitecore Habitat |
	

	@NeedImplementation
Scenario: Demo_Adwords link on the Google search results mockup
Show that the campaign is triggered on the website
	Given Mockup of Google page is opened
	When Actor enters any text to search field
	And clicks Google Search button
	Then search field contains following text
	| Text             |
	| Sitecore Habitat |
	And link with following parametr is present on the page
	| Text                                            |
	| ?sc_camp={0BFFAF94-F523-452A-9F2A-1FA3292D4647} |


	@NeedImplementation
Scenario: Demo_Campaign is triggered on the website
	Given link with following parametr is clicked 
	| Text                                            |
	| ?sc_camp={0BFFAF94-F523-452A-9F2A-1FA3292D4647} |
	When Actor clicks info icon in the bottom right corner
	Then flyout with following sections is opened
	| Section Name  |
	| Contact       |
	| This visist   |
	And Camapign drop-down  contains following text
	| Text                     |
	| Facebook Content Messages|

	
	