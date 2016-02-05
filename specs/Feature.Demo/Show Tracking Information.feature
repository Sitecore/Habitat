Feature: Show Tracking Information
	

@NeedImplementation
Scenario: Demo_Show Tracking Information_UC1_Expand Slide bar with tracking info
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner
	Then Slide bar element was expanded


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC1_Collapse Slide bar with tracking info
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner
	And User clicks on <Info-sign> in the right down corner
	Then Slide bar element was collapsed
	
	
@NeedImplementation
Scenario: Demo_Show Tracking Information_UC2_Contact_First Visit
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.   
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner
	Then System shows following info on Contact panel title
	| Number of visits | Engagement value |
	| 1                | 10               |


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC2_Contact_Few Visits
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.   
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner
	And User clicks END VISIT button
	And User clicks on <Info-sign> in the right down corner
	Then System shows following info on Contact panel title
	| Number of visits | Engagement value |
	| 2                | 20               | 


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC3_Contact_Identification 
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner
	And User expands Identification section
	Then Contact ID term with it's ID presents
	And Identification status is <Known>


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC3_Contact_Identification_new session 
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner
	And User records Contact ID 
	And User cleans up browser cookies 
	And User refreshes browser page
	And User clicks on <Info-sign> in the right down corner
	And User expands Identification section
	Then New Contact ID presents 
	And Identifier is empty
	And Identification status is <None>
	 	 

@NeedImplementation
Scenario: Demo_Show Tracking Information_UC4_This Visit_First Visit 
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner
	Then Following This Visit section contains
	| Pages Viewed | Engagement value |
	| 1            | 10               |


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC5_This Visit_Few Visits
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner
	And User clicks END VISIT button
	And User clicks on <Info-sign> in the right down corner
	Then Following This Visit section contains
	| Pages Viewed | Engagement value |
	| 2            | 20               |


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC5_This Visit_Few Visits and pages
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner
	And User clicks END VISIT button
	And User selects CONTACT US navigation menu
	And User clicks on <Info-sign> in the right down corner
	Then Following This Visit section contains
	| Pages Viewed | Engagement value |
	| 2            | 10               |


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC6_Patterns_Focus Developer  
	Given Habitat website is opened on Main Page
	When Actor moves cursor over the MODULES
	And User selects PROJECT navigation menu
    And User clicks on MODULES link in breadcrumbs 
	And User clicks on <Info-sign> in the right down corner
	And User expands Patterns section
	Then Following Patterns section contains 
	| Text H4 | Text H5   |
	| Focus   | Developer | 


	@NeedImplementation
Scenario: Demo_Show Tracking Information_UC6_Experience Profile_Focus Developer  
	Given Habitat website is opened on Main Page
	When Actor moves cursor over the MODULES
	And User selects PROJECT navigation menu
    And User clicks on MODULES link in breadcrumbs 
	And User clicks on <Info-sign> in the right down corner
	And User expands Experience Profile section
	And User expands Focus subsection
	Then Following Focus subsection contains 
	| Scores        |
	| background: 3 |
	| practical: 5  |
	| process: 7    |
	| scope: 2      |

@NeedImplementation
Scenario: Demo_Show Tracking Information_UC6_Patterns_Focus SolutionArchitect   
	Given Habitat website is opened on Main Page
	When Actor moves cursor over the ABOUT HABITAT
	And User selects INTRODUCTION navigation menu
	And User clicks on <Info-sign> in the right down corner 
	And User expands Patterns section
	Then Following Patterns section contains 
	| Text H4 | Text H5           |
	| Focus   | SolutionArchitect | 


@NeedImplementation 
Scenario: Demo_Show Tracking Information_UC7_Campaigns
	Given Habitat website is opened on Main Page
	When User opens following link http://habitat.test5ua1.dk.sitecore.net/TrackerData.aspx
	And User clicks <Track campaign> button
	And User opens Habitat Main Page
	And User clicks on <Info-sign> in the right down corner 
	And User expands Campaigns section
	Then Following Campaigns section contains
	| Campaign name |
	| Register page |


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC8_Geo IP Location
	Given Habitat website is opened on Main Page
	When User opens following link http://habitat.test5ua1.dk.sitecore.net/TrackerData.aspx
	And User clicks <Set GeoIp> button
	And User opens Habitat Main Page
	And User clicks on <Info-sign> in the right down corner 
	And User expands Geo IP Location section
	Then Following Geo IP Location section contains
	| City  | Postal Code |
	| Miami | 33129       |


@NeedImplementation 
Scenario: Demo_Show Tracking Information_UC9_Device
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner 
	And User expands Device section
	Then Following Device section contains
	| Device Type | Vendor | Operating System        | Browser             | Display Height | Display Width |
	| Computer    | Misc   | Windows 8.1 (Microsoft) | Internet Explorer 7 | Unknown        | Unknown       |


@NeedImplemantation 
Scenario: Demo_Show Tracking Information_UC10_Pages_Shown pages 
	Given Habitat website is opened on Main Page
	When User selects MORE INFO navigation menu
	And User selects PROJECT navigation menu
	And User selects default image
	And User clicks on <Info-sign> in the right down corner
	And User expands Pages section
	Then Following Pages section contains 
	| Page link  |
	| Home       |
	| /Project   |
	| More-Info  |
	| Home       |


@NeedImplemantation 
Scenario: Demo_Show Tracking Information_UC10_Pages_Click link to page 
	Given Habitat website is opened on Main Page
	When User selects MORE INFO navigation menu
	And User selects HOME navigation menu
	And User clicks on <Info-sign> in the right down corner
	And User expands Pages section
	And User selects More-Info from the Pages section
	Then Page URL ends on /More-Info
	And <Find out more> title presents on page


@NeedImplementation 
Scenario: Demo_Show Tracking Information_UC11_Goals_Goals archived 
	Given Habitat website is opened on Main Page
	When Actor moves cursor over the User icon
	And Actor selects Register from drop-down menu
	And Actor fill in all required fields
	|Email             |Password |Confirm password |  
	| ace@sitecore.net | a       | a               |
	And Actor clicks Register button
	And User clicks on <Info-sign> in the right down corner
	And User expands Goals section
	Then Following Goals section contains
	| Goal name with score |
	| Login (0)            |
	| Register (0)         |


@NeedImplementation 
Scenario: Demo_Show Tracking Information_UC12_Engagement          
	Given Habitat website is opened on Main Page
	And following user is logged in
	| Username            | Password |
	| JohnSmith@gmail.com | j        |
	When User clicks on <Info-sign> in the right down corner 
	And User expands Engagement section
	Then Following Engagement section contains
	| Plan               | Last state      |
	| Register Page Open | Registered user |


@NeedImplementation
Scenario: Demo_Show Tracking Information_Bug36059_Tracking information panel is empty in Experience Editor
	Given Admin user is logged into Habitat
	When Admin launches <Experience Editor>
	Then there is no <Info-sign> in the right down corner of the page