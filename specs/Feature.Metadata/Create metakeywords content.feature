@UI
Feature: Create metakeywords content

	As an editor 
	I want to easily select meta keywords 
	so that I assure that the right keywords are used across the site

@Scenario below for manual testing
#Scenario: Create metakeywords content_UC1_Create new keyword in the Content Editor
#	Given Admin user is logged into Content Editor application
#	When Admin opens following item
#	| Item Path                                           |
#	| /sitecore/content/Habitat/Global/Metadata Keywords  |
#	And Admin selects Metakeyword from Insert chunk on the ribbon
#	And Admin enters <Name> on the Message dialog
#	And Admin clicks OK
#	Then new item with title <Name> should be added under following item
#	| Item Path                                           |
#	| /sitecore/content/Habitat/Global/Metadata Keywords  |

@Ready for Automating testing
Scenario: Create metakeywords content_UC1_Create new keyword in the Content Editor
	Given The following Metadata keywords are defined in Sitecore
	| Item Path                                                   | 
	| /sitecore/content/Habitat/Global/Metadata Keywords/habitat  |
	| /sitecore/content/Habitat/Global/Metadata Keywords/sitecore |
	
	And Admin create a new Metakeyword
	| Item Path                                          | FieldName  | FieldValue |
	| /sitecore/content/Habitat/Global/Metadata Keywords | NewKeyWord |   		/sitecore/templates/Project/Common/Content Types/MetaKeyword          |
	Then new item with title NewKeyWord should be added under following item
	| Item Path                                                     |  
	| /sitecore/content/Habitat/Global/Metadata Keywords/habitat    | 
	| /sitecore/content/Habitat/Global/Metadata Keywords/sitecore   | 
	| /sitecore/content/Habitat/Global/Metadata Keywords/NewKeyWord |

@Scenario below for manual testing
#Scenario: Create metakeywords content_UC2_Assign metakeyword to page and check keywords in the page HTML
#	Given Admin user is logged into Content Editor application
#	When Admin opens following item
#	| Item Path                                                    | Field Name    |
#	| /sitecore/content/Habitat/Home/About Habitat/Getting Started | Meta Keywords |
#	And Admin assign <sitecore> keyword
#	And Admin saves changes on the item
#	Then page HTML contains following tag
#	| Tag                                       |
#	| <meta name="keywords" content="sitecore"> |

@Ready for Automating testing
Scenario: Create metakeywords content_UC2_Assign metakeyword to page and check keywords in the page HTML
	Given The following Metadata keywords are defined in Sitecore
	| Item Path                                                    | FieldName    | FieldValue |
	| /sitecore/content/Habitat/Home/About Habitat/Getting Started | MetaKeywords |  habitat   |
	| /sitecore/content/Habitat/Home/About Habitat/Getting Started | MetaKeywords |  sitecore  |
	When The sitecore keyword has been selected
	| ItemPath                                                     | FieldName     | FieldValue                             |
	| /sitecore/content/Habitat/Home/About Habitat/Getting Started | MetaKeywords | {153640E2-FD0D-42D3-8480-F9615EE78A9A} |
	Then Habitat website is opened on Getting Started page
	And The following tag is present
	| Item name|
	| sitecore |

@Scenario below for manual testing
#Scenario: Create metakeywords content_UC3_Assign several metakeywords to page and check keywords in the page HTML
#	Given Admin user is logged into Content Editor application
#	When Admin opens following item
#	| Item Path                                                    | Field Name    |
#	| /sitecore/content/Habitat/Home/About Habitat/Getting Started | Meta Keywords |
#	And Admin assign <sitecore> keyword
#	And Admin assign <habitat> keyword
#	And Admin saves changes on the item
#	Then page HTML contains following tag
#	|Tag                                                |
#	|<meta name="keywords" content="sitecore, habitat"> |

@Ready for Automating testing
Scenario: Create metakeywords content_UC3_Assign metakeyword to page and check keywords in the page HTML
	Given The following Metadata keywords are defined in Sitecore
	| Item Path                                                    | FieldName    | FieldValue |
	| /sitecore/content/Habitat/Home/About Habitat/Getting Started | MetaKeywords |  habitat   |
	| /sitecore/content/Habitat/Home/About Habitat/Getting Started | MetaKeywords |  sitecore  |
	When The sitecore keyword has been selected
	| Item Path                                                    | FieldName    | FieldValue                               |
	| /sitecore/content/Habitat/Home/About Habitat/Getting Started | MetaKeywords | {425DF582-C8D5-4EBA-B23E-FA1A069435DD}\|{153640E2-FD0D-42D3-8480-F9615EE78A9A} |
	Then Habitat website is opened on Getting Started page
	And The following tag is present
	| Item name|
	| habitat  |
	| sitecore |
