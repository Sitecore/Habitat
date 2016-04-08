Feature: Mockup of External page

In order to start a demo story from an external page
As a technical presales consultant
I want to be able to show a mockup of an external page, e.g. search engine page with adword links to a campaign on the website

	@Ready
Scenario: Demo_UC1_Show Google search engine mockup
	Given Habitat website is opened on Main Page
	When Actor navigates to Demo site 
	Then URl contains Demo site url
	And Demo site title equals to <Google>
	And Following buttons are present on the Demo site page
	| Goggle page buttons |
	| Google Search       |
	| I'm Feeling Lucky   |

	
	@Ready
Scenario: Demo_UC2_Show predefined keyword
	Given Mockup of Google page is opened
	When Actor enters test search text in to search field
	And Actor clicks Google Search button 
	Then Search results contains following sitelink
	| Text                                                      |
	| Sitecore Habitat - Flexibility, Simplicity, Extensibility‎ |

	

	@Ready
Scenario: Demo_UC3_Adwords link on the Google search results mockup
	Given Mockup of Google page is opened
	When Actor enters new test search text in to search field
	And Actor clicks Google Search button
	And Actor clicks Sitecore Habitat - Flexibility, Simplicity, Extensibility‎ link
	Then Page url contains Campaign ID



	@Ready
Scenario: Demo_UC4_Campaign is triggered on the website
	Given Mockup of Google page is opened
	When Actor enters new test search text in to search field
	And Actor clicks Google Search button
	And Actor clicks Sitecore Habitat - Flexibility, Simplicity, Extensibility‎ link
#	Then Campaign presents on Campaign section of the Show Tracking Info
#	| Campaign                  |
#	| Facebook Content Messages |

	
	