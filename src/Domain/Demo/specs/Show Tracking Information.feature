Feature: Show Tracking Information
	
#Sitecore package "TestData_Show Tracking Information.zip" should be uploaded to habitat before test passing 

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

	 
@NeedImplementation
Scenario: Demo_Show Tracking Information_UC4_Contact_Classification_List of the available check boxes(Unclassified)  
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.   
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner
	And User expands Classification section
	Then Following check boxes present in list
	| Area Name           | Checkbox state |
	| Unclassified        | checked        |
	| Business Visitor    | unchecked      |
	| ISP                 | unchecked      |
	| Existing Customer   | unchecked      |
	| Analyst             | unchecked      |
	| Press               | unchecked      |
	| Supplier            | unchecked      |
	| Business Partner    | unchecked      |
	| Competitor          | unchecked      |
	| My Company          | unchecked      |
	| Bot - Feed Reader   | unchecked      |
	| Bot - Search Engine | unchecked      |
	| Bot - Unidentified  | unchecked      |
	| Bot - Auto-detected | unchecked      |
	| Bot - Malicious     | unchecked      |


@InDesign
Scenario: Demo_Show Tracking Information_UC4_Contact_Classification_Existing customer checked
#Engagement value should be predefined in Goal and published. Also Goal is assigned to Home page and published.   
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	When User clicks on <Info-sign> in the right down corner
	And User expands Classification section
	Then Following check boxes present in list
	| Area Name           | Checkbox state |
	| Unclassified        | unchecked      |
	| Business Visitor    | unchecked      |
	| ISP                 | unchecked      |
	| Existing Customer   | checked        |
	| Analyst             | unchecked      |
	| Press               | unchecked      |
	| Supplier            | unchecked      |
	| Business Partner    | unchecked      |
	| Competitor          | unchecked      |
	| My Company          | unchecked      |
	| Bot - Feed Reader   | unchecked      |
	| Bot - Search Engine | unchecked      |
	| Bot - Unidentified  | unchecked      |
	| Bot - Auto-detected | unchecked      |
	| Bot - Malicious     | unchecked      |
	 
		 

@NeedImplementation
Scenario: Demo_Show Tracking Information_UC5_This Visit_First Visit 
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
	| 1            | 10               |


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
	When Actor moves cursor over the Modules
	And User selects PROJECT navigation menu
	And User selects default image
	And User clicks on Website list-group-item
	And User clicks on Design list-group-item  
	And User clicks on <Info-sign> in the right down corner
	And User expands Patterns section
	Then Following image presents: http://habitat.test5ua1.dk.sitecore.net/-/media/Habitat/Images/Square/Habitat-067-square.jpg?mw=50&w=50&hash=EDC92E237DDCFE910A6CD27BCA2530938E06AE6D
	And Following Patterns section contains 
	| Text H4 | Text H5   |
	| FOCUS   | DEVELOPER | 


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC6_Patterns_Focus Architect   
	Given Habitat website is opened on Main Page
	When Actor moves cursor over the ABOUT HABITAT
	And User selects Fundamentals navigation menu
	And User clicks on Flexibility link
	And User clicks on Extensibility link
	And User clicks on Cimplicity link
	And User clicks on <Info-sign> in the right down corner 
	And User expands Patterns section
	Then Following image presents: http://habitat.test5ua1.dk.sitecore.net/-/media/Habitat/Images/Square/Habitat-080-square.jpg?mw=50&w=50&hash=2C014C2D7867CA4532759638430D9EAD55038059 
	And Following Patterns section contains 
	| Text H4 | Text H5   |
	| FOCUS   | ARCHITECT | 


@Need Implimentation
Scenario: Demo_Show Tracking Information_UC7_Campaigns
	Given Habitat website is opened on /?sc_camp=9AC6AD85B15A4A5BB14337185A19364E
	When User clicks on <Info-sign> in the right down corner 
	And User expands Campaigns section
	Then Following Campaigns section contains
	| Campaign name         |
	| Habitat Test Campaign |

@InDesign
Scenario: Demo_Show Tracking Information_UC8_Geo IP Location
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner 
	And User expands Geo IP Location section
	Then <I have no idea how to test it now>


@NeedImplementation
Scenario: Demo_Show Tracking Information_UC9_Device
	Given Habitat website is opened on Main Page
	When User clicks on <Info-sign> in the right down corner 
	And User expands Device section
	Then Following Device section contains
	| Device Type | Vendor | Operating System      | Browser      | Display Height | Display Width |
	| Computer    | Misc   | Windows 7 (Microsoft) | Unidentified | Unknown        | Unknown       |




@NeedImplemantation 
Scenario: Demo_Show Tracking Information_UC10_Pages_Shown pages 
	Given Habitat website is opened on Main Page
	When User selects CONTACT US navigation menu
	And User selects PROJECT navigation menu
	And User selects default image
	And User clicks on <Info-sign> in the right down corner
	And User expands Pages section
	Then Following Pages section contains 
	| Page link  |
	| /Website   |
	| /Project   |
	| Contact-Us |
	| Home       |


@NeedImplemantation 
Scenario: Demo_Show Tracking Information_UC10_Pages_Click link to page 
	Given Habitat website is opened on Main Page
	When User selects CONTACT US navigation menu
	And User selects HOME navigation menu
	And User clicks on <Info-sign> in the right down corner
	And User expands Pages section
	And User selects Contact Us from the Pages section
	Then Page URL ends on /Contact-Us
	And Contact Us title presents on page


@NeedImplemantation 
Scenario: Demo_Show Tracking Information_UC11_Goals_Goals archived 
	Given Habitat website is opened on Main Page
	When Actor moves cursor over the User icon
	And User selects Register from drop-down menu
	And User clicks on <Info-sign> in the right down corner
	And User expands Goals section
	Then Following Goals section contains
	| Goal name with score |
	| Register page (20)   |
	| Home Page (10)       |


Register?sc_camp=9AC6AD85B15A4A5BB14337185A19364E


@Need Implimentation
Scenario: Demo_Show Tracking Information_UC12_Engagement          
	Given Habitat website is opened on /Register?sc_camp=9AC6AD85B15A4A5BB14337185A19364E
	When User clicks on <Info-sign> in the right down corner 
	And User expands Engagement section
	Then Following Engagement section contains
	| Engagement plan name |
	| Register Page Open   |