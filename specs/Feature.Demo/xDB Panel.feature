Feature: xDB Panel
	

@NeedImplementation
Scenario: xDB Panel_UC1_Open Main Panel view
	Given Habitat website is opened on Main Page
	When Actor selects <Open visit details panel> slidebar
	Then xDB panel has opened
	And xDB panel contains following headers
	| Header name          |
	| Engagement           |
	| Personal Information |
	| Onsite Behavior      |
	| Referral             |


@NeedImplementation
Scenario: xDB Panel_UC2_Close Panel
	Given Habitat website is opened on Main Page
	And xDB panel has opened
	When Actor clicks <Close> button on xDB panel
	Then xDB panel is closed  


@NeedImplementation
Scenario: xDB Panel_UC3_Refresh visit details panel
	Given Habitat website is opened on Main Page
	And xDB panel has opened
	When Actor opens Habitat Main page in a new tab
	And Actor registers a new User in Habitat
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And Actor returns to previous tab 
	And Actor clicks <Refresh> button on xDB panel
	And Actor expands <Onsite Behavior> header on xDB panel
	Then Trigger Goals field contains values
	| Triggered goal |
	| Register       |
	| Login          |
	 


@NeedImplementation
Scenario: xDB Panel_UC4_End visit
	 Given Habitat website is opened on Main Page
	 And xDB panel has opened
	 When Actor clicks <End Visit> button on xDB panel
	 And Actor selects <Open visit details panel> slidebar
	 And Actor expands Engagement header on xDB panel
	 Then <Engagement header> contains following values
	 | Number of visits |
	 | 2                |  


@NeedImplementation
Scenario: xDB Panel_UC5_Engagement header_Main view
	Given Habitat website is opened on Main Page
	When Actor selects <Open visit details panel> slidebar
	Then <Engagement header> contains following values
	| Engagement value | Pages Viewed | Number of visits |
	| 0                | 1            | 2                |


@NeedImplementation
Scenario: xDB Panel_UC6_Engagement header_Engagement value_First open
	 Given Habitat website is opened on Main Page
	 And xDB panel has opened
	 When Actor expands <Engagement header> on xDB panel
	 Then <Engagement value> field contains
	 | field value |
	 | 0           |



@Design starts after Bug 37402 is implemented 
Scenario: xDB Panel_UC7_Engagement header_Engagement value_Triggered value


@NeedImplementation
Scenario: xDB Panel_UC8_Engagement header_Pages seen in this visit_Ten latest pages
	Given Habitat website is opened on Main Page
	When Actor goes to More-Info page
	And Actor goes to Feature page
	And Actor goes to Person page
	And Actor goes to Employees-List page
	And Actor goes to Main page
	And Actor goes to Register page
	And Actor goes to Login page
	And Actor goes to Getting-Started page
	And Actor goes to Main page
	And Actor goes to More-Info page 
	And Actor selects <Open visit details panel> slidebar
	And Actor expands <Engagement header> on xDB panel
	Then <Pages seen in this visit> field contains
	| field value                            |
	| /More-Info                             |
	| /Modules/Feature                       |
	| /Modules/Feature/Person                |
	| /Modules/Feature/Person/Employees-List |
	| /Home                                  |
	| /Register                              |
	| /Login                                 |
	| /Getting-Started                       |
	| /Home                                  |
	| /More-Info                             |
	And Some pages have info about time actor had spent on page 


@NeedImplementation
Scenario: xDB Panel_UC9_Engagement header_Visits to the site
	Given Habitat website is opened on Main Page
	And xDB panel has opened
	When Actor clicks <End Visit> button on xDB panel
	And Actor selects <Open visit details panel> slidebar
	And Actor clicks <End Visit> button on xDB panel
	And Actor selects <Open visit details panel> slidebar
	And Actor expands <Engagement header> on xDB panel
	Then <Visits to the site> field contains
	| field value |
	| 3           |
	And <Engagement header> contains following values
	| Number of visits |
	| 2                |


@NeedImplementation
Scenario: xDB Panel_UC10_Personal Information header_Contact identification_Anonymus
	Given Habitat website is opened on Main Page
	When Actor selects <Open visit details panel> slidebar
	And Actor expands <Personal Information> header on xDB panel
	Then <Personal Information> contains following values
	| Contact identification |
	| You are anonymous      |



@Design starts after Bug 37402 is implemented 
Scenario: xDB Panel_UC11_Personal Information header_Contact identification_Registered user


@NeedImplementation 
Scenario: xDB Panel_UC12_Personal Information header_Device detection 
	Given Habitat website is opened on Main Page
	When Actor selects <Open visit details panel> slidebar
	And Actor expands <Personal Information> header on xDB panel
	Then <Device> contains following values
	| Device           | Browser      |
	| Misc, Windows PC | Unidentified |



@NeedImplementation 
Scenario: xDB Panel_UC13_Onsite Behavior_Profiling_Contact have not been profiled
	Given Habitat website is opened on Main Page
	When Actor selects <Open visit details panel> slidebar
	And Actor expands <Personal Information> header on xDB panel
	Then <Profiling> contains following values
	| field value                    |
	| You have not been profiled yet |



@Design starts after Bug 37402 is implemented 
Scenario: xDB Panel_UC14_Onsite Behavior_Profiling_Contact profile
	 


@NeedImplementation 		
Scenario: xDB Panel_UC15_Onsite Behavior_Triggered goals_No triggered goals
	Given Habitat website is opened on Main Page
	When Actor selects <Open visit details panel> slidebar
	And Actor expands <Personal Information> header on xDB panel
	Then <Triggered goals> contains following values
	| field value                        |
	| You have triggered no goals so far |


@NeedImplementation 		
Scenario: xDB Panel_UC16_Onsite Behavior_Triggered goals_Trigger
	Given Habitat website is opened on Main Page
	And xDB panel has opened
	When Actor registers a new User in Habitat
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |
	And Actor logged out 
	And Actor logins to Habitat with a user
	| Email              | Password |
	| kov10@sitecore.net | k        |
	And Actor expands <Onsite Behavior> header on xDB panel
	Then <Triggered goals> contains following values
	| Triggered goal |
	| Register       |
	| Login          |
	| Login          |



@NeedImplementation 		
Scenario: xDB Panel_UC17_Onsite Behavior_Outcomes_No outcomes have been reached 
	Given Habitat website is opened on Main Page
	And xDB panel has opened
	When Actor expands <Onsite Behavior> header on xDB panel
	Then <Outcomes> contains following values
	| field value                  |
	| You have reached no outcomes |





@NeedImplementation 		
Scenario: xDB Panel_UC18_Onsite Behavior_Outcomes_Triggered outcome 
	Given Outcome set to item field
	| ItemPath                  | FieldName       | FieldValue          |
	| /sitecore/content/Habitat | RegisterOutcome | Outcomes/Sales Lead |
	And Habitat website is opened on Main Page
	And xDB panel has opened
	When Actor expands <Onsite Behavior> header on xDB panel
	Then <Outcomes> contains following values
	| field value |
	| Sales Lead  |


@NeedImplementation 		
Scenario: xDB Panel_UC19_Referral_Referrer_Direct referrer
	 Given Habitat website is opened on Main Page
	 And xDB panel has opened
	 When Actor expands <Referral> header on xDB panel
	 Then <Outcomes> contains following values
	 | field value    |
	 | Direct traffic |



@NeedImplementation 		
Scenario: xDB Panel_UC20_Referral_Referrer_Referrel after second visit 
	 Given Habitat website is opened on Main Page
	 And xDB panel has opened
	 When Actor clicks <End Visit> button on xDB panel
	 And Actor selects <Open visit details panel> slidebar
	 And Actor expands <Referral> header on xDB panel
	 Then <Outcomes> contains following values
	 | field value         |
	 | Habitat website url |



@Bug 35882: 		
Scenario: xDB Panel_UC21_Referral_Referrer_From another site
	Given Mockup of Google page is opened
	When Actor enters new test search text in to search field
	And Actor clicks Google Search button
	And Actor clicks Sitecore Habitat - Flexibility, Simplicity, Extensibility‎ link
	And Actor selects <Open visit details panel> slidebar
	 And Actor expands <Referral> header on xDB panel
	 Then <Outcomes> contains following values
	 | field value      |
	 | Demo website url |



@NeedImplementation 		
Scenario: xDB Panel_UC22_Referral_Campaigns_No associated campaigns
	Given Habitat website is opened on Main Page
	And Actor selects <Open visit details panel> slidebar
	And Actor expands <Referral> header on xDB panel
	Then <Campaigns> contains following values
	| field value                                     |
	| You have not been associated with any campaigns |


@Bug 35882: 		
Scenario: xDB Panel_UC23_Referral_Campaigns_No associated campaigns
	Given Mockup of Google page is opened
	When Actor enters new test search text in to search field
	And Actor clicks Google Search button
	And Actor clicks Sitecore Habitat - Flexibility, Simplicity, Extensibility‎ link
	And Actor selects <Open visit details panel> slidebar
	And Actor expands <Referral> header on xDB panel
	Then <Campaigns> contains following values
	| field value               |
	| Facebook Content Messages |


@NeedImplementation 		
Scenario: xDB Panel_UC24_Available on another page
	 Given Habitat website is opened on Feature page 
	 When Actor selects <Open visit details panel> slidebar
	Then xDB panel has opened
	And xDB panel contains following headers
	| Header name          |
	| Engagement           |
	| Personal Information |
	| Onsite Behavior      |
	| Referral             |



@NeedImplementation 		
Scenario: xDB Panel_UC25_Not available in Edit mode
	Given User registered and login on Habitat website
	When  User opens Main Page and switch  to Edit mode
	Then <Open visit details panel> icon is available
	 
@NeedImplementation
Scenario: xDB Panel_UC26_Not available in Preview mode
    Given User registered and login on Habitat website
	When  User opens Main Page and switch to Preview mode
	Then <Open visit details panel> icon is available
	 
@NeedImplementation
Scenario: xDB Panel_UC27_Not available in Explore mode
	Given User registered and login on Habitat website
	When  User opens Main Page and switch to Explore mode
	Then <Open visit details panel> icon is available
	 
@NeedImplementation
Scenario: xDB Panel_UC28_Not available in Debug mode
	Given User registered and login on Habitat website
	When  User opens Main Page and switch to Debug mode
	Then <Open visit details panel> icon is available


@Need design after Bug 37617 is fixed		
Scenario: xDB Panel_UC29_Tooltips
