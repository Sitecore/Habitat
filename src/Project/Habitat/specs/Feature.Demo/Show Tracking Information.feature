@UI
Feature: Show Tracking Information
	

@NeedImplementation
Scenario: Demo_Show Tracking Information_UC1_Expand Slide bar with tracking info
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	Then Slide bar element is visible 


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC2_Collapse Slide bar with tracking info
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	And User clicks on <Info-sign> in the right part of the screen 
	Then Slide bar element is invisible
	
	
@NeedImplementation
Scenario: Demo_Show Tracking Information_UC3_Contact_NumberOfVisits_First Visit
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.   
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	Then System shows following number of visits for the Contact
	| Number of visits |
	| 1                |


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC4_Contact_NumberOfVisits_Few Visits
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.   
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	And User clicks END VISIT button
	And User clicks on <Info-sign> in the right part of the screen
	Then System shows following number of visits for the Contact
	| Number of visits |
	| 2                |
	
	
@NeedImplementation
Scenario: Demo_Show Tracking Information_UC5_Contact_EngagementValue
	Given Habitat website is opened on Main Page
	And User was Login to Habitat
	| Email               | Password |
	| JohnSmith@gmail.com | j        |
	When User clicks on <Info-sign> in the right part of the screen
	Then System shows following Engagement value for the Contact
	| Engagement value |
	|                  |	 


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC6_Contact_Identification 
	Given Habitat website is opened on Main Page
	And User was Login to Habitat
	| Email               | Password |
	| JohnSmith@gmail.com | j        |
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Identification section
	Then Following info showns for Contact--Identification section
	| Contact ID                           | Identifier                   | Identification status |
	| e5cb9cce-3397-4b40-af6b-f1e5bf3400bc | extranet\JohnSmith@gmail.com | Known                 |




@NeedImplementation
Scenario: Demo_Show Tracking Information_UC7_Contact_Identification_new session 
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Identification section
	Then Following info showns for Contact--Identification section
	| Identification status |
	| None                  |


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC8_Contact_PersonalData_Data exists
	Given Habitat website is opened on Main Page
	And User was Login to Habitat
	| Email               | Password |
	| JohnSmith@gmail.com | j        |
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Personal Data section
	Then Following info showns for Contact--Personal Data section
	| DT text       | DD text                            |
	| Picture       |                                    |
	| First Name    | John                               |
	| Surname       | Smith                              |
	| Gender        | Male                               |
	| Job Title     | Intern                             |
	| Addresses     | USA, Miami, 153 SE, 15th Rd, 33129 |
	| Phone Numbers |                                    |
	| cell          | 775  45454456                      |
	| work          | 775 (15) 3434567653                |  

@NeedImplementation
Scenario: Demo_Show Tracking Information_UC9_Contact_PersonalData_Data does not exist
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	Then No text under Personal Data section


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC10_Contact_Communication_Data exists
	Given Habitat website is opened on Main Page
	And User was Login to Habitat
	| Email               | Password |
	| JohnSmith@gmail.com | j        |
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Communication section
	Then Following info showns for Contact--Communication section
	| DT text               | DD text             |
	| Communication Revoked | False               |
	| Consent Revoked       | False               |
	| Email Addresses       |                     |
	| Primary               | JohnSmith@gmail.com |
	| Preference Language   | en                  |


@InDesign
Scenario: Demo_Show Tracking Information_UC11_Contact_Communication_Data does not exist
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Communication section
	Then Following info showns for Contact--Communication section
	| DT text               | DD text |
	| Communication Revoked | False   |
	| Consent Revoked       | False   |


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC12_Contact_BehaviorProfiles_Data exists
	Given Habitat website is opened on Main Page
	And User was Login to Habitat
	| Email               | Password |
	| JohnSmith@gmail.com | j        |
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Behavior Profiles section
	Then Panel Title contains
	| Profile name |
	| Focus        |
	And Following info showns for Contact--Behavior Profiles section
	| DT text                | DD text                              |
	| Id                     | 24dff2cf-b30a-4b75-8967-2fe3ded82271 |
	| Number Of Times Scored | 3                                    |
	| Scores                 | Background: 18                       |
	|                        | Practical: 12                        |
	|                        | Process: 14                          |
	|                        | Scope: 11                            |



@NeedImplementation
Scenario: Demo_Show Tracking Information_UC13_Contact_BehaviorProfiles_Data does not exist
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Behavior Profiles section
	Then No text under Behavior Profiles section

@NeedImplementation
Scenario: Demo_Show Tracking Information_UC14_Contact_KeyBehaviorCache
	Given Habitat website is opened on Main Page
	And User was Login to Habitat
	| Email               | Password |
	| JohnSmith@gmail.com | j        |
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Key Behavior Cache section
	Then Key Behavior Cache Title contains
	| Profile name |
	| Channels     |
	| Goals        |
	| Page Events  |
                  



@NeedImplementation
Scenario: Demo_Show Tracking Information_UC15_Contact_KeyBehaviorCache_Data does not exist
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Key Behavior Cache section
	Then No text under Behavior Profiles section
	 	 

@NeedImplementation
Scenario: Demo_Show Tracking Information_UC16_This Visit_First Visit 
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	Then Following This Visit section contains
	| Pages Viewed | Engagement value |
	| 1            | 10               |


@InDesign-Need to update after test data inserted
Scenario: Demo_Show Tracking Information_UC17_This Visit_Few Visits
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	And User clicks END VISIT button
	And User clicks on <Info-sign> in the right part of the screen
	Then Following This Visit section contains
	| Pages Viewed | Engagement value |
	| 2            | 20               |


@InDesign-Need to update after test data inserted
Scenario: Demo_Show Tracking Information_UC18_This Visit_Few Visits and pages
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	And User clicks END VISIT button
	And User selects CONTACT US navigation menu
	And User clicks on <Info-sign> in the right down corner
	Then Following This Visit section contains
	| Pages Viewed | Engagement value |
	| 2            | 10               |


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC19_Patterns_Patterns nearly matched   
	Given Habitat website is opened on Main Page
	And User was Login to Habitat
	| Email               | Password |
	| JohnSmith@gmail.com | j        |
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Patterns section
	Then Patterns section main pattern
	| Text H4 | Text H5           | Progress |
	| Focus   | SolutionArchitect | 56.50%   | 	
	And Patterns section <Other patterns matches>
	| Text H5   | Progress |
	| Developer | 43.50%   |


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC20_Patterns_Data does not exist
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Patterns section
	Then No text under Patterns section


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC20_ExperienceProfile_Focus Developer  
	Given Habitat website is opened on Main Page
	And User was Login to Habitat
	| Email               | Password |
	| JohnSmith@gmail.com | j        |
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Experience Profile section
	Then Following Experience Profile section contains 
	| Text H4 | Text H5   |
	| Focus   | Developer | 
	And Following Focus subsection contains 
	| Scores        |
	| background: 3 |
	| practical: 5  |
	| process: 7    |
	| scope: 2      |


@NeedImplementation 
Scenario: Demo_Show Tracking Information_UC21_Campaigns
	Given Habitat website is opened on Main Page
	When User opens following link http://habitat.test5ua1.dk.sitecore.net/TrackerData.aspx
	And User clicks <Track campaign> button
	And User opens Habitat Main Page
	And User clicks on <Info-sign> in the right part of the screen
	And User expands Campaigns section
	Then Following Campaigns section contains
	| Campaign name |
	| Register page |


@NeedImplementation 
Scenario: Demo_Show Tracking Information_UC22_Campaigns_Data does not exist
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Campaigns section
	Then No text under Campaignss section



@NeedImplementation
Scenario: Demo_Show Tracking Information_UC23_GeoIPLocation
	Given Habitat website is opened on Main Page
	When User opens following link http://habitat.test5ua1.dk.sitecore.net/TrackerData.aspx
	And User clicks <Set GeoIp> button
	And User opens Habitat Main Page
	And User clicks on <Info-sign> in the right part of the screen
	And User expands Geo IP Location section
	Then Following Geo IP Location section contains
	| City  | Postal Code |
	| Miami | 33129       |

@NeedImplementation 
Scenario: Demo_Show Tracking Information_UC24_GeoIPLocation_Data does not exist
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Geo IP Location section
	Then No text under Geo IP Location section



@NeedImplementation 
Scenario: Demo_Show Tracking Information_UC24_Device
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right part of the screen 
	And User expands Device section
	Then Following Device section contains
	| Device Type | Vendor | Operating System        | Browser      | Display Height | Display Width |
	| Computer    | Misc   | Windows 8.1 (Microsoft) | Unidentified | Unknown        | Unknown       |


@NeedImplemantation 
Scenario: Demo_Show Tracking Information_UC25_Pages_Shown pages 
	Given Habitat website is opened on Main Page
	When User selects MORE INFO navigation menu
	And User selects PROJECT navigation menu
	And User selects default image
	And User clicks on <Info-sign> in the right part of the screen
	And User expands Pages section
	Then Following Pages section contains 
	| Page link  |
	| Home       |
	| /Project   |
	| More-Info  |
	| Home       |


@NeedImplemantation 
Scenario: Demo_Show Tracking Information_UC26_Pages_Click link to page 
	Given Habitat website is opened on Main Page
	When User selects MORE INFO navigation menu
	And User selects HOME navigation menu
	And User clicks on <Info-sign> in the right part of the screen
	And User expands Pages section
	And User selects More-Info from the Pages section
	Then Page URL ends on /More-Info
	And <Find out more> title presents on page


@NeedImplementation 
Scenario: Demo_Show Tracking Information_UC27_Goals_Goals archived 
	Given Habitat website is opened on Main Page
	When Actor moves cursor over the User icon
	And Actor selects Register from drop-down menu
	And Actor fill in all required fields
	|Email             |Password |Confirm password |  
	| ace@sitecore.net | a       | a               |
	And Actor clicks Register button
	And User clicks on <Info-sign> in the right part of the screen
	And User expands Goals section
	Then Following Goals section contains
	| Goal name with score |
	| Login (0)            |
	| Register (0)         |


@NeedImplementation 
Scenario: Demo_Show Tracking Information_UC28_Engagement          
	Given Habitat website is opened on Main Page
	And User was Login to Habitat
	| Email               | Password |
	| JohnSmith@gmail.com | j        |
	When User clicks on <Info-sign> in the right part of the screen
	And User expands Engagement section
	Then Following Engagement section contains
	| Plan               | Last state      |
	| Register Page Open | Registered user |


@NeedImplememntation
Scenario: Demo_Show Tracking Information_UC29_Refresh button
	Given Habitat website is opened on Main Page
	When Actor clicks on <Info-sign> in the right part of the screen
	And Actor expands <Pages> section
	And Actor collaps sidebar
	And Actor opens <More Info> page in another browser window in the same session
	And Actor clicks on <Info-sign> in the right part of the screen
	And Actor clicks <Refresh> button on the left side of the sidebar
	And Actor expands <Pages> section
	Then Then Following Pages section contains 
	| Page link  |
	| Home       |
	| More-Info  |


@NeedImplementation
Scenario: Demo_Show Tracking Information_Bug36059_Tracking information panel is empty in Experience Editor
	Given Admin user is logged into Habitat
	When Admin launches <Experience Editor>
	Then there is no <Info-sign> in the right part of the screen


