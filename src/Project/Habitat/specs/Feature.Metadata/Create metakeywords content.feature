Feature: Create metakeywords content

	As an editor 
	I want to easily select meta keywords 
	so that I assure that the right keywords are used across the site

@NeedImplementation
Scenario: Create metakeywords content_UC1_Create new keyword in the Content Editor
	Given Admin user is logged into Content Editor application
	When Admin opens following item
	| Item Path                                           |
	| /sitecore/content/Habitat/Global/Metadata Keywords  |
	And Admin selects Metakeyword from Insert chunk on the ribbon
	And Admin enters <Name> on the Message dialog
	And Admin clicks OK
	Then new item with title <Name> should be added under following item
	| Item Path                                           |
	| /sitecore/content/Habitat/Global/Metadata Keywords  |

@NeedImplementation
Scenario: Create metakeywords content_UC2_Assign metakeyword to page and check keywords in the page HTML
	Given Admin user is logged into Content Editor application
	When Admin opens following item
	| Item Path                                                    | Field Name    |
	| /sitecore/content/Habitat/Home/About Habitat/Getting Started | Meta Keywords |
	And Admin assign <sitecore> keyword
	And Admin saves changes on the item
	Then page HTML contains following tag
	| Tag                                       |
	| <meta name="keywords" content="sitecore"> |

@NeedImplementation
Scenario: Create metakeywords content_UC3_Assign several metakeywords to page and check keywords in the page HTML
	Given Admin user is logged into Content Editor application
	When Admin opens following item
	| Item Path                                                    | Field Name    |
	| /sitecore/content/Habitat/Home/About Habitat/Getting Started | Meta Keywords |
	And Admin assign <sitecore> keyword
	And Admin assign <habitat> keyword
	And Admin saves changes on the item
	Then page HTML contains following tag
	|Tag                                                |
	|<meta name="keywords" content="sitecore, habitat"> |
