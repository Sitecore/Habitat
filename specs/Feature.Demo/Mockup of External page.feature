Feature: Mockup of External page

In order to start a demo story from an external page
As a technical presales consultant
I want to be able to show a mockup of an external page, e.g. search engine page with adword links to a campaign on the website

	@NeedImplementation
Scenario: Demo_UC1_Show Google search engine mockup
	Given Habitat website is opened on Main Page
	When Actor navigates to Demo site 
	Then URl contains Demo site url
	And Demo site title equals to <Google>
	And Following buttons are present on the Demo site page
	| Goggle page buttons |
	| Google Search       |
	| I'm Feeling Lucky   |

	
	@NeedImplementation
Scenario: Demo_UC2_Show predefined keyword
	Given Mockup of Google page is opened
	When Actor enters test search text in to search field
	And Actor clicks Google Search button 
	Then Search results contains following sitelink
	| Text                                                      |
	| Sitecore Habitat - Flexibility, Simplicity, Extensibility‎ |

	

	@NeedImplementation
Scenario: Demo_UC3_Adwords link on the Google search results mockup
Show that the campaign is triggered on the website
	Given Mockup of Google page is opened
	When Actor enters new test search text in to search field
	And Actor clicks Google Search button
	Then Search results contains following sitelink
	| Text                                                      |
	| Sitecore Habitat - Flexibility, Simplicity, Extensibility‎ | 
	And link with following parametr is present on the page
	| Text                                            |
	| ?sc_camp={0BFFAF94-F523-452A-9F2A-1FA3292D4647} |


	@NeedImplementation
Scenario: Demo_UC4_Campaign is triggered on the website
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

	
	